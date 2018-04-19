using IPROJ.Contracts.DataModel;
using Microsoft.EntityFrameworkCore;

namespace IPROJ.MSSQLRepository.Context
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

        public virtual DbSet<DeviceDescription> Devices { get; set; }

        public virtual DbSet<DeviceReading> DeviceReadings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceDescription>()
            .Property(b => b.DeviceId).HasColumnName("Description");

            modelBuilder.Entity<DeviceDescription>().ToTable("Devices");

            modelBuilder.Entity<DeviceReading>().Ignore(_ => _.DeviceState);

            modelBuilder.Entity<DeviceDescription>(entity =>
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

                entity.Property(e => e.TypeOfDevice)
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

                entity.Ignore(e => e.Device);
            });
        }
    }
}
