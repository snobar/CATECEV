using CATECEV.Models.Entity;
using CATECEV.Models.Entity.AMPECO.Resources.ChargePoint;
using CATECEV.Models.Entity.AMPECO.Resources.Session;
using CATECEV.Models.Entity.AMPECO.Resources.Tax;
using CATECEV.Models.Entity.AMPECO.Resources.User;
using CATECEV.Models.Entity.Shared;
using Microsoft.EntityFrameworkCore;

namespace CATECEV.Data.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Company> Company { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<VehicleUser> VehicleUser { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserOptions> UserOptions { get; set; }
        public DbSet<Lookups> Lookups { get; set; }
        public DbSet<LookupCategory> LookupCategory { get; set; }
        public DbSet<ChargingSessionEntity> ChargingSession { get; set; }
        public DbSet<ChargePointEntity> ChargePoint { get; set; }
        public DbSet<EvseEntity> Evse { get; set; }
        public DbSet<TaxEntity> Tax { get; set; }
        public DbSet<TaxDisplayNameEntity> TaxDisplayName { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Front End
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Vehicles)
                      .WithOne(u => u.Company)
                      .HasForeignKey(e => e.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(e => e.Country)
                      .WithMany(c => c.Companies)
                      .HasForeignKey(e => e.CountryId)
                      .OnDelete(DeleteBehavior.SetNull);


                entity.HasOne(e => e.City)
                      .WithMany(c => c.Companies)
                      .HasForeignKey(e => e.CityId)
                      .OnDelete(DeleteBehavior.SetNull);
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

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id);


                entity.HasMany(e => e.Cities)
                      .WithOne(c => c.Country)
                      .HasForeignKey(c => c.CountryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);

            });
            #endregion

            #region Shared
            modelBuilder.Entity<Lookups>(entity =>
            {
                entity.ToTable("Lookups", "Shared");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.LookupCategory)
                      .WithMany(u => u.Lookups)
                      .HasForeignKey(e => e.LookupCategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasData(new Lookups
                {
                    Id = (int)Models.Enums.PaymentStatus.Paid,
                    LookupCategoryId = (int)Models.Enums.LookupCategory.PaymentStatus,
                    EnglishDescription = "Paid",
                    ArabicDescription = "مدفوع",
                    IsActive = true,
                    OrderId = 1
                });
            });

            modelBuilder.Entity<LookupCategory>(entity =>
            {
                entity.ToTable("LookupCategory", "Shared");
                entity.HasKey(e => e.Id);

                entity.HasData(new LookupCategory
                {
                    Id = (int)Models.Enums.LookupCategory.PaymentStatus,
                    Description = "Payment Status",
                    IsActive = true,
                });
            });
            #endregion

            #region User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "Resources");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Options)
                      .WithOne(u => u.User)
                      .HasForeignKey<User>(e => e.OptionsId)
                      .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<UserOptions>(entity =>
            {
                entity.ToTable("UserOptions", "Resources");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.User)
                      .WithOne(u => u.Options)
                      .HasForeignKey<UserOptions>(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

            });
            #endregion

            #region ChargingSession
            modelBuilder.Entity<ChargingSessionEntity>(entity =>
            {
                entity.ToTable("ChargingSession", "Resources");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Tax)
                      .WithMany(u => u.ChargingSession)
                      .HasForeignKey(e => e.TaxId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.ChargingSessions)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Evse)
                      .WithMany(u => u.ChargingSessions)
                      .HasForeignKey(e => e.EvseId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Connector)
                      .WithMany(u => u.ChargingSessions)
                      .HasForeignKey(e => e.ConnectorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            #endregion

            #region ChargePoint
            modelBuilder.Entity<ChargePointEntity>(entity =>
            {
                entity.ToTable("ChargePoint", "Resources");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.OwnerUser)
                      .WithMany(u => u.ChargePoint)
                      .HasForeignKey(e => e.OwnerUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ConnectorEntity>(entity =>
            {
                entity.ToTable("Connector", "Resources");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Evse)
                      .WithMany(u => u.Connectors)
                      .HasForeignKey(e => e.EvseId)
                      .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<EvseEntity>(entity =>
            {
                entity.ToTable("Evse", "Resources");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.ChargePoint)
                      .WithMany(u => u.Evses)
                      .HasForeignKey(e => e.ChargePointId)
                      .OnDelete(DeleteBehavior.Restrict);

            });
            #endregion

            #region Tax
            modelBuilder.Entity<TaxEntity>(entity =>
            {
                entity.ToTable("Tax", "Resources");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<TaxDisplayNameEntity>(entity =>
            {
                entity.ToTable("TaxDisplayName", "Resources");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Tax)
                      .WithMany(u => u.DisplayName)
                      .HasForeignKey(e => e.TaxId)
                      .OnDelete(DeleteBehavior.Restrict);

            });
            #endregion
        }
    }
}
