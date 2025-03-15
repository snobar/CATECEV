using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CATECEV.Data.Migrations
{
    /// <inheritdoc />
    public partial class _999 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                schema: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AMPECOId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAlwaysOpen = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoamingOperatorId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationAdditionalDescription",
                schema: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Locale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Translation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationAdditionalDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationAdditionalDescription_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Resources",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationAddress",
                schema: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Locale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Translation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationAddress_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Resources",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationDescription",
                schema: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Locale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Translation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationDescription_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Resources",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationName",
                schema: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Locale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Translation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationName_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Resources",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationShortDescription",
                schema: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Locale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Translation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationShortDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationShortDescription_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Resources",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationAdditionalDescription_LocationId",
                schema: "Resources",
                table: "LocationAdditionalDescription",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationAddress_LocationId",
                schema: "Resources",
                table: "LocationAddress",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationDescription_LocationId",
                schema: "Resources",
                table: "LocationDescription",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationName_LocationId",
                schema: "Resources",
                table: "LocationName",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationShortDescription_LocationId",
                schema: "Resources",
                table: "LocationShortDescription",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationAdditionalDescription",
                schema: "Resources");

            migrationBuilder.DropTable(
                name: "LocationAddress",
                schema: "Resources");

            migrationBuilder.DropTable(
                name: "LocationDescription",
                schema: "Resources");

            migrationBuilder.DropTable(
                name: "LocationName",
                schema: "Resources");

            migrationBuilder.DropTable(
                name: "LocationShortDescription",
                schema: "Resources");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "Resources");
        }
    }
}
