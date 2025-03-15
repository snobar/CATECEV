using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CATECEV.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingAuthorizationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorizationId",
                schema: "Resources",
                table: "ChargingSession",
                newName: "AMPECOAuthorizationId");

            migrationBuilder.CreateTable(
                name: "Authorization",
                schema: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AMPECOId = table.Column<int>(type: "int", nullable: false),
                    AMPECOUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTagUid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Roaming = table.Column<bool>(type: "bit", nullable: true),
                    AMPECOChargePointId = table.Column<int>(type: "int", nullable: false),
                    AMPECOEvseId = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authorization_ChargePoint_AMPECOChargePointId",
                        column: x => x.AMPECOChargePointId,
                        principalSchema: "Resources",
                        principalTable: "ChargePoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Authorization_Evse_AMPECOEvseId",
                        column: x => x.AMPECOEvseId,
                        principalSchema: "Resources",
                        principalTable: "Evse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Authorization_User_AMPECOUserId",
                        column: x => x.AMPECOUserId,
                        principalSchema: "Resources",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChargingSession_AMPECOAuthorizationId",
                schema: "Resources",
                table: "ChargingSession",
                column: "AMPECOAuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_AMPECOChargePointId",
                schema: "Resources",
                table: "Authorization",
                column: "AMPECOChargePointId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_AMPECOEvseId",
                schema: "Resources",
                table: "Authorization",
                column: "AMPECOEvseId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_AMPECOUserId",
                schema: "Resources",
                table: "Authorization",
                column: "AMPECOUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChargingSession_Authorization_AMPECOAuthorizationId",
                schema: "Resources",
                table: "ChargingSession",
                column: "AMPECOAuthorizationId",
                principalSchema: "Resources",
                principalTable: "Authorization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargingSession_Authorization_AMPECOAuthorizationId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.DropTable(
                name: "Authorization",
                schema: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_ChargingSession_AMPECOAuthorizationId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.RenameColumn(
                name: "AMPECOAuthorizationId",
                schema: "Resources",
                table: "ChargingSession",
                newName: "AuthorizationId");
        }
    }
}
