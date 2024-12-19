using hijazi.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace hijazi.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Company> Company { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<VehicleUser> VehicleUser { get; set; }

        public DbSet<VehicleType> VehicleType { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Vehicles)
                      .WithOne(u => u.Company)
                      .HasForeignKey(e => e.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Company)
                      .WithMany(u => u.Vehicles)
                      .HasForeignKey(e => e.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.VehicleType)
                     .WithMany(u => u.Vehicles)
                     .HasForeignKey(e => e.TypeId)
                     .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.VehicleUsers)
                      .WithOne(u => u.Vehicle)
                      .HasForeignKey(e => e.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<VehicleUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Vehicle)
                      .WithMany(u => u.VehicleUsers)
                      .HasForeignKey(e => e.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Vehicles)
                      .WithOne(u => u.VehicleType)
                      .HasForeignKey(e => e.TypeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
