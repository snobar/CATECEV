using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CATECEV.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingAuthorizationTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargingSession_Authorization_AMPECOAuthorizationId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.DropIndex(
                name: "IX_ChargingSession_AMPECOAuthorizationId",
                schema: "Resources",
                table: "ChargingSession");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ChargingSession_AMPECOAuthorizationId",
                schema: "Resources",
                table: "ChargingSession",
                column: "AMPECOAuthorizationId");

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
    }
}
