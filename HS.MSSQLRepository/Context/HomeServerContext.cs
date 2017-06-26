using HS.MSSQLRepository.ModelData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HS.MSSQLRepository.Context
{
    public partial class HomeServerContext : DbContext
    {
        private readonly string _connectionString;
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<InstrumentReadings> InstrumentReadings { get; set; }

        public HomeServerContext()
            : this(@"Data Source=KOMP;Initial Catalog=HomeServer;Integrated Security=True")
        { }

        public HomeServerContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Devices>(entity =>
            {
                entity.HasKey(e => e.DeviceId)
                    .HasName("PK__Devices__49E12331436B04CD");

                entity.Property(e => e.DeviceId)
                    .HasColumnName("DeviceID")
                    .HasDefaultValueSql("newid()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ReadingInterval).HasDefaultValueSql("0");

                entity.Property(e => e.TypeOfReading)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<InstrumentReadings>(entity =>
            {
                entity.HasKey(e => new { e.ReadingTimeStamp, e.DeviceId })
                    .HasName("PK__Instrume__C83D229E9B3C7148");

                entity.Property(e => e.ReadingTimeStamp).HasColumnType("datetime");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.Value).HasColumnType("decimal");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.InstrumentReadings)
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Instrumen__Devic__3C34F16F");
            });
        }
    }
}