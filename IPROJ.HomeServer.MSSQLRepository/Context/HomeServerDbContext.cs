using IPROJ.Contracts.DataModel;
using Microsoft.EntityFrameworkCore;

namespace IPROJ.HomeServer.MSSQLRepository
{
    public class HomeServerDbContext : DbContext
    {
        private readonly string _connectionString;

        public HomeServerDbContext()
        {
            _connectionString = @"Data Source=KOMP;Initial Catalog=HomeServer;Integrated Security=True";
        }

        public HomeServerDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual DbSet<Device> Devices { get; set; }

        public virtual DbSet<DeviceReading> DeviceReadings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
            .Property(b => b.DeviceId).HasColumnName("Description");

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(e => e.DeviceId);

                entity.Property(e => e.DeviceId)
                    .HasColumnName("DeviceID")
                    .HasDefaultValueSql("newid()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TypeOfReading)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.CustomId)
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Host)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<DeviceReading>(entity =>
            {
                entity.HasKey(e => new { e.ReadingTimeStamp, e.DeviceId });

                entity.Property(e => e.ReadingTimeStamp).HasColumnType("datetime");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.Value).HasColumnType("decimal");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.DeviceReadings)
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.TypeOfReading)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });
        }
    }
}
