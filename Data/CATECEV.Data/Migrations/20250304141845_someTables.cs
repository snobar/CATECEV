using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CATECEV.Data.Migrations
{
    /// <inheritdoc />
    public partial class someTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChargePoint",
                schema: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AMPECOId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkId = table.Column<int>(type: "int", nullable: true),
                    NetworkProtocol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkPort = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HardwareStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlugAndCharge = table.Column<bool>(type: "bit", nullable: false),
                    LastBootNotification = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccessType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargingProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentSecurityProfile = table.Column<int>(type: "int", nullable: false),
                    HardwareEnabledSecurityProfile = table.Column<int>(type: "int", nullable: true),
                    DesiredSecurityProfile = table.Column<int>(type: "int", nullable: false),
                    DesiredSecurityProfileStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoamingOperatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerPartnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerPartnerContractId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerCorporateBillingAsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Capabilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstConnection = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerUserId = table.Column<int>(type: "int", nullable: true),
                    ManagedByOperator = table.Column<bool>(type: "bit", nullable: false),
                    AutoFaultRecovery = table.Column<bool>(type: "bit", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayTariffsAndCosts = table.Column<bool>(type: "bit", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionRequired = table.Column<bool>(type: "bit", nullable: false),
                    SubscriptionPlanIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargePoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargePoint_User_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalSchema: "Resources",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AMPECOId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    TaxIdentificationNumberId = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChargingSessionId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evse",
                schema: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AMPECOId = table.Column<int>(type: "int", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxPower = table.Column<int>(type: "int", nullable: false),
                    MaxVoltage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxAmperage = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HardwareStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MidMeterCertificationEndYear = table.Column<int>(type: "int", nullable: true),
                    TariffGroupId = table.Column<int>(type: "int", nullable: false),
                    ChargingProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowsReservation = table.Column<bool>(type: "bit", nullable: false),
                    RoamingPhysicalReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TariffIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capabilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoamingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargePointId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evse_ChargePoint_ChargePointId",
                        column: x => x.ChargePointId,
                        principalSchema: "Resources",
                        principalTable: "ChargePoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxDisplayNameEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Locale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Translation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxDisplayNameEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxDisplayNameEntity_TaxEntity_TaxId",
                        column: x => x.TaxId,
                        principalTable: "TaxEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Connector",
                schema: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AMPECOId = table.Column<int>(type: "int", nullable: false),
                    EvseId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connector", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Connector_Evse_EvseId",
                        column: x => x.EvseId,
                        principalSchema: "Resources",
                        principalTable: "Evse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChargingSession",
                schema: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AMPECOId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChargePointId = table.Column<int>(type: "int", nullable: false),
                    EvseId = table.Column<int>(type: "int", nullable: false),
                    ConnectorId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StoppedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Energy = table.Column<int>(type: "int", nullable: false),
                    PowerKw = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NonBillableEnergy = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethodId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminalId = table.Column<int>(type: "int", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorizationId = table.Column<int>(type: "int", nullable: false),
                    IdTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTagLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtendingSessionId = table.Column<int>(type: "int", nullable: true),
                    ReimbursementEligibility = table.Column<bool>(type: "bit", nullable: false),
                    TariffSnapshotId = table.Column<int>(type: "int", nullable: false),
                    ElectricityCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExternalSessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvsePhysicalReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentStatusUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    BillingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmountWithTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmountWithoutTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxId = table.Column<int>(type: "int", nullable: false),
                    TaxPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalEnergyConsumption = table.Column<int>(type: "int", nullable: false),
                    EnergyConsumptionGrid = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargingSession_ChargePoint_ChargePointId",
                        column: x => x.ChargePointId,
                        principalSchema: "Resources",
                        principalTable: "ChargePoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChargingSession_Connector_ConnectorId",
                        column: x => x.ConnectorId,
                        principalSchema: "Resources",
                        principalTable: "Connector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChargingSession_Evse_EvseId",
                        column: x => x.EvseId,
                        principalSchema: "Resources",
                        principalTable: "Evse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChargingSession_TaxEntity_TaxId",
                        column: x => x.TaxId,
                        principalTable: "TaxEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChargingSession_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Resources",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChargePoint_OwnerUserId",
                schema: "Resources",
                table: "ChargePoint",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingSession_ChargePointId",
                schema: "Resources",
                table: "ChargingSession",
                column: "ChargePointId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingSession_ConnectorId",
                schema: "Resources",
                table: "ChargingSession",
                column: "ConnectorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingSession_EvseId",
                schema: "Resources",
                table: "ChargingSession",
                column: "EvseId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingSession_TaxId",
                schema: "Resources",
                table: "ChargingSession",
                column: "TaxId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChargingSession_UserId",
                schema: "Resources",
                table: "ChargingSession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Connector_EvseId",
                schema: "Resources",
                table: "Connector",
                column: "EvseId");

            migrationBuilder.CreateIndex(
                name: "IX_Evse_ChargePointId",
                schema: "Resources",
                table: "Evse",
                column: "ChargePointId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxDisplayNameEntity_TaxId",
                table: "TaxDisplayNameEntity",
                column: "TaxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargingSession",
                schema: "Resources");

            migrationBuilder.DropTable(
                name: "TaxDisplayNameEntity");

            migrationBuilder.DropTable(
                name: "Connector",
                schema: "Resources");

            migrationBuilder.DropTable(
                name: "TaxEntity");

            migrationBuilder.DropTable(
                name: "Evse",
                schema: "Resources");

            migrationBuilder.DropTable(
                name: "ChargePoint",
                schema: "Resources");
        }
    }
}
