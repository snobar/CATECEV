﻿// <auto-generated />
using System;
using CATECEV.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CATECEV.Data.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AMPECOId")
                        .HasColumnType("int");

                    b.Property<string>("AccessType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("AutoFaultRecovery")
                        .HasColumnType("bit");

                    b.PrimitiveCollection<string>("Capabilities")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChargingProfile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentSecurityProfile")
                        .HasColumnType("int");

                    b.Property<int>("DesiredSecurityProfile")
                        .HasColumnType("int");

                    b.Property<string>("DesiredSecurityProfileStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DisplayTariffsAndCosts")
                        .HasColumnType("bit");

                    b.Property<string>("ExternalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FirstConnection")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HardwareEnabledSecurityProfile")
                        .HasColumnType("int");

                    b.Property<string>("HardwareStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastBootNotification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<bool>("ManagedByOperator")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NetworkId")
                        .HasColumnType("int");

                    b.Property<string>("NetworkIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NetworkPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NetworkPort")
                        .HasColumnType("int");

                    b.Property<string>("NetworkProtocol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NetworkStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerPartnerContractId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerPartnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OwnerUserId")
                        .HasColumnType("int");

                    b.Property<bool>("PartnerCorporateBillingAsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Pin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PlugAndCharge")
                        .HasColumnType("bit");

                    b.Property<string>("RoamingOperatorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("SubscriptionPlanIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SubscriptionRequired")
                        .HasColumnType("bit");

                    b.PrimitiveCollection<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("ChargePoint", "Resources");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ConnectorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AMPECOId")
                        .HasColumnType("int");

                    b.Property<int>("EvseId")
                        .HasColumnType("int");

                    b.Property<string>("Format")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EvseId");

                    b.ToTable("Connector", "Resources");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AMPECOId")
                        .HasColumnType("int");

                    b.Property<bool>("AllowsReservation")
                        .HasColumnType("bit");

                    b.PrimitiveCollection<string>("Capabilities")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChargePointId")
                        .HasColumnType("int");

                    b.Property<string>("ChargingProfile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HardwareStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("MaxAmperage")
                        .HasColumnType("int");

                    b.Property<int>("MaxPower")
                        .HasColumnType("int");

                    b.Property<string>("MaxVoltage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MidMeterCertificationEndYear")
                        .HasColumnType("int");

                    b.Property<string>("NetworkId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysicalReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoamingPhysicalReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoamingStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TariffGroupId")
                        .HasColumnType("int");

                    b.PrimitiveCollection<string>("TariffIds")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChargePointId");

                    b.ToTable("Evse", "Resources");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AMPECOId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("AuthorizationId")
                        .HasColumnType("int");

                    b.Property<string>("BillingStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChargePointId")
                        .HasColumnType("int");

                    b.Property<int?>("ConnectorId")
                        .HasColumnType("int");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ElectricityCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Energy")
                        .HasColumnType("int");

                    b.Property<int>("EnergyConsumptionGrid")
                        .HasColumnType("int");

                    b.Property<int>("EvseId")
                        .HasColumnType("int");

                    b.Property<string>("EvsePhysicalReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExtendingSessionId")
                        .HasColumnType("int");

                    b.Property<string>("ExternalSessionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdTag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdTagLabel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("NonBillableEnergy")
                        .HasColumnType("int");

                    b.Property<string>("PaymentMethodId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PaymentStatusUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PowerKw")
                        .HasColumnType("float");

                    b.Property<int>("ReceiptId")
                        .HasColumnType("int");

                    b.Property<bool>("ReimbursementEligibility")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StoppedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("TariffSnapshotId")
                        .HasColumnType("int");

                    b.Property<int>("TaxId")
                        .HasColumnType("int");

                    b.Property<decimal>("TaxPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("TerminalId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmountWithTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalAmountWithoutTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TotalEnergyConsumption")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChargePointId");

                    b.HasIndex("ConnectorId");

                    b.HasIndex("EvseId");

                    b.HasIndex("TaxId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("ChargingSession", "Resources");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxDisplayNameEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaxId")
                        .HasColumnType("int");

                    b.Property<string>("Translation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TaxId");

                    b.ToTable("TaxDisplayNameEntity");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AMPECOId")
                        .HasColumnType("int");

                    b.Property<int>("ChargingSessionId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.Property<int>("TaxIdentificationNumberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TaxEntity");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.User.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AMPECOId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EmailVerified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExternalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OptionsId")
                        .HasColumnType("int");

                    b.Property<string>("PersonalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ReceiveNewsAndPromotions")
                        .HasColumnType("bit");

                    b.Property<bool>("RequirePasswordReset")
                        .HasColumnType("bit");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubscriptionId")
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("TermsAndPoliciesIdsWithConsent")
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("UserGroupIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", "Resources");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.User.UserOptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("SessionsAllowed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserOptions", "Resources");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ArabicName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("EnglishName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyRegistrationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TopUpAmount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ArabicName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnglishName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.Shared.LookupCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("LookupCategory", "Shared");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Payment Status",
                            IsActive = true
                        });
                });

            modelBuilder.Entity("CATECEV.Models.Entity.Shared.Lookups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ArabicDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnglishDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("LookupCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LookupCategoryId");

                    b.ToTable("Lookups", "Shared");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArabicDescription = "مدفوع",
                            EnglishDescription = "Paid",
                            IsActive = true,
                            LookupCategoryId = 1,
                            OrderId = 1
                        });
                });

            modelBuilder.Entity("CATECEV.Models.Entity.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MACAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("VINNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("TypeId");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.VehicleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ArabicName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnglishName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("VehicleType");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.VehicleUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ArabicName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnglishName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleUser");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity", b =>
                {
                    b.HasOne("CATECEV.Models.Entity.AMPECO.Resources.User.User", "OwnerUser")
                        .WithMany("ChargePoint")
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("OwnerUser");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ConnectorEntity", b =>
                {
                    b.HasOne("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity", "Evse")
                        .WithMany("Connectors")
                        .HasForeignKey("EvseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Evse");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity", b =>
                {
                    b.HasOne("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity", "ChargePoint")
                        .WithMany("Evses")
                        .HasForeignKey("ChargePointId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChargePoint");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity", b =>
                {
                    b.HasOne("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity", "ChargePoint")
                        .WithMany("ChargingSessions")
                        .HasForeignKey("ChargePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ConnectorEntity", "Connector")
                        .WithMany("ChargingSessions")
                        .HasForeignKey("ConnectorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity", "Evse")
                        .WithMany("ChargingSessions")
                        .HasForeignKey("EvseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxEntity", "Tax")
                        .WithOne("ChargingSession")
                        .HasForeignKey("CATECEV.Models.Entity.AMPECO.Resources.Session.ChargingSessionEntity", "TaxId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CATECEV.Models.Entity.AMPECO.Resources.User.User", "User")
                        .WithMany("ChargingSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChargePoint");

                    b.Navigation("Connector");

                    b.Navigation("Evse");

                    b.Navigation("Tax");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxDisplayNameEntity", b =>
                {
                    b.HasOne("CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxEntity", "Tax")
                        .WithMany("DisplayName")
                        .HasForeignKey("TaxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tax");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.User.UserOptions", b =>
                {
                    b.HasOne("CATECEV.Models.Entity.AMPECO.Resources.User.User", "User")
                        .WithOne("Options")
                        .HasForeignKey("CATECEV.Models.Entity.AMPECO.Resources.User.UserOptions", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.City", b =>
                {
                    b.HasOne("CATECEV.Models.Entity.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.Company", b =>
                {
                    b.HasOne("CATECEV.Models.Entity.City", "City")
                        .WithMany("Companies")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("CATECEV.Models.Entity.Country", "Country")
                        .WithMany("Companies")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("City");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.Shared.Lookups", b =>
                {
                    b.HasOne("CATECEV.Models.Entity.Shared.LookupCategory", "LookupCategory")
                        .WithMany("Lookups")
                        .HasForeignKey("LookupCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("LookupCategory");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.Vehicle", b =>
                {
                    b.HasOne("CATECEV.Models.Entity.Company", "Company")
                        .WithMany("Vehicles")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CATECEV.Models.Entity.VehicleType", "VehicleType")
                        .WithMany("Vehicles")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("VehicleType");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.VehicleUser", b =>
                {
                    b.HasOne("CATECEV.Models.Entity.Vehicle", "Vehicle")
                        .WithMany("VehicleUsers")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ChargePointEntity", b =>
                {
                    b.Navigation("ChargingSessions");

                    b.Navigation("Evses");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.ConnectorEntity", b =>
                {
                    b.Navigation("ChargingSessions");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.ChargePoint.EvseEntity", b =>
                {
                    b.Navigation("ChargingSessions");

                    b.Navigation("Connectors");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.Tax.TaxEntity", b =>
                {
                    b.Navigation("ChargingSession");

                    b.Navigation("DisplayName");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.AMPECO.Resources.User.User", b =>
                {
                    b.Navigation("ChargePoint");

                    b.Navigation("ChargingSessions");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.City", b =>
                {
                    b.Navigation("Companies");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.Company", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.Country", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("Companies");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.Shared.LookupCategory", b =>
                {
                    b.Navigation("Lookups");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.Vehicle", b =>
                {
                    b.Navigation("VehicleUsers");
                });

            modelBuilder.Entity("CATECEV.Models.Entity.VehicleType", b =>
                {
                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
