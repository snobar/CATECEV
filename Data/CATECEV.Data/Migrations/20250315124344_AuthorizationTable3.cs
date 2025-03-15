using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CATECEV.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuthorizationTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authorization_ChargePoint_AMPECOChargePointId",
                schema: "Resources",
                table: "Authorization");

            migrationBuilder.DropForeignKey(
                name: "FK_Authorization_Evse_AMPECOEvseId",
                schema: "Resources",
                table: "Authorization");

            migrationBuilder.DropForeignKey(
                name: "FK_Authorization_User_AMPECOUserId",
                schema: "Resources",
                table: "Authorization");

            migrationBuilder.DropIndex(
                name: "IX_Authorization_AMPECOChargePointId",
                schema: "Resources",
                table: "Authorization");

            migrationBuilder.DropIndex(
                name: "IX_Authorization_AMPECOEvseId",
                schema: "Resources",
                table: "Authorization");

            migrationBuilder.DropIndex(
                name: "IX_Authorization_AMPECOUserId",
                schema: "Resources",
                table: "Authorization");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Authorization_ChargePoint_AMPECOChargePointId",
                schema: "Resources",
                table: "Authorization",
                column: "AMPECOChargePointId",
                principalSchema: "Resources",
                principalTable: "ChargePoint",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Authorization_Evse_AMPECOEvseId",
                schema: "Resources",
                table: "Authorization",
                column: "AMPECOEvseId",
                principalSchema: "Resources",
                principalTable: "Evse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Authorization_User_AMPECOUserId",
                schema: "Resources",
                table: "Authorization",
                column: "AMPECOUserId",
                principalSchema: "Resources",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
