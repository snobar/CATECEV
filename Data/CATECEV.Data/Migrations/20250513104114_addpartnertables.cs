using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CATECEV.Data.Migrations
{
    /// <inheritdoc />
    public partial class addpartnertables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "VehicleUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "VehicleType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "UserOptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "UserGroup",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "TaxDisplayName",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "Tax",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Shared",
                table: "Lookups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Shared",
                table: "LookupCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "LocationShortDescription",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "LocationName",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "LocationDescription",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "LocationAddress",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "LocationAdditionalDescription",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "Location",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "Evse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "Connector",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "City",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "ChargingSession",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "ChargePoint",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                schema: "Resources",
                table: "Authorization",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CorporateBilling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    MonthlyLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateBilling", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartnerOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateUsers = table.Column<bool>(type: "bit", nullable: false),
                    AddUserBalance = table.Column<bool>(type: "bit", nullable: false),
                    SupplierOnReceipts = table.Column<bool>(type: "bit", nullable: false),
                    AllowToControlTariffs = table.Column<bool>(type: "bit", nullable: false),
                    AllowToControlTariffGroups = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaultNotificationsEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonthlyPlatformFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    CorporateBillingId = table.Column<int>(type: "int", nullable: true),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partner_CorporateBilling_CorporateBillingId",
                        column: x => x.CorporateBillingId,
                        principalTable: "CorporateBilling",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Partner_PartnerOptions_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "PartnerOptions",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "Shared",
                table: "LookupCategory",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Shared",
                table: "Lookups",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Partner_CorporateBillingId",
                table: "Partner",
                column: "CorporateBillingId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_OptionsId",
                table: "Partner",
                column: "OptionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partner");

            migrationBuilder.DropTable(
                name: "CorporateBilling");

            migrationBuilder.DropTable(
                name: "PartnerOptions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "VehicleUser");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "VehicleType");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "UserOptions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "UserGroup");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "TaxDisplayName");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Shared",
                table: "Lookups");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Shared",
                table: "LookupCategory");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "LocationShortDescription");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "LocationName");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "LocationDescription");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "LocationAddress");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "LocationAdditionalDescription");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "Evse");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "Connector");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "City");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "ChargePoint");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Resources",
                table: "Authorization");
        }
    }
}
