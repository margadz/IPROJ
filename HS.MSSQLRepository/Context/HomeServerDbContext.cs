using System;
using System.Collections.Generic;
using System.Text;
using IPROJ.Contracts.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HS.MSSQLRepository.Context
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
