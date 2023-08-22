using EatonTest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EatonTest.Infrastructure
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "EatonTestDb");
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasIndex(d => d.Name)
                .IsUnique();

            modelBuilder.Entity<Device>()
                .HasMany(x => x.Measurments)
                .WithOne(x => x.Device)
                .HasForeignKey(r => r.DeviceId);
        }

        public DbSet<Device> Devices { get; set; }

        public DbSet<Measurement> Measurements { get; set; }
    }
}
