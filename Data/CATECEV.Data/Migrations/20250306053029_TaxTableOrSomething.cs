using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CATECEV.Data.Migrations
{
    /// <inheritdoc />
    public partial class TaxTableOrSomething : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargingSession_TaxEntity_TaxId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxDisplayNameEntity_TaxEntity_TaxId",
                table: "TaxDisplayNameEntity");

            migrationBuilder.DropIndex(
                name: "IX_ChargingSession_TaxId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxEntity",
                table: "TaxEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxDisplayNameEntity",
                table: "TaxDisplayNameEntity");

            migrationBuilder.DropColumn(
                name: "ChargingSessionId",
                table: "TaxEntity");

            migrationBuilder.RenameTable(
                name: "TaxEntity",
                newName: "Tax",
                newSchema: "Resources");

            migrationBuilder.RenameTable(
                name: "TaxDisplayNameEntity",
                newName: "TaxDisplayName",
                newSchema: "Resources");

            migrationBuilder.RenameIndex(
                name: "IX_TaxDisplayNameEntity_TaxId",
                schema: "Resources",
                table: "TaxDisplayName",
                newName: "IX_TaxDisplayName_TaxId");

            migrationBuilder.AlterColumn<int>(
                name: "AMPECOId",
                schema: "Resources",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tax",
                schema: "Resources",
                table: "Tax",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxDisplayName",
                schema: "Resources",
                table: "TaxDisplayName",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingSession_TaxId",
                schema: "Resources",
                table: "ChargingSession",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChargingSession_Tax_TaxId",
                schema: "Resources",
                table: "ChargingSession",
                column: "TaxId",
                principalSchema: "Resources",
                principalTable: "Tax",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxDisplayName_Tax_TaxId",
                schema: "Resources",
                table: "TaxDisplayName",
                column: "TaxId",
                principalSchema: "Resources",
                principalTable: "Tax",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargingSession_Tax_TaxId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxDisplayName_Tax_TaxId",
                schema: "Resources",
                table: "TaxDisplayName");

            migrationBuilder.DropIndex(
                name: "IX_ChargingSession_TaxId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxDisplayName",
                schema: "Resources",
                table: "TaxDisplayName");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tax",
                schema: "Resources",
                table: "Tax");

            migrationBuilder.RenameTable(
                name: "TaxDisplayName",
                schema: "Resources",
                newName: "TaxDisplayNameEntity");

            migrationBuilder.RenameTable(
                name: "Tax",
                schema: "Resources",
                newName: "TaxEntity");

            migrationBuilder.RenameIndex(
                name: "IX_TaxDisplayName_TaxId",
                table: "TaxDisplayNameEntity",
                newName: "IX_TaxDisplayNameEntity_TaxId");

            migrationBuilder.AlterColumn<string>(
                name: "AMPECOId",
                schema: "Resources",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ChargingSessionId",
                table: "TaxEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxDisplayNameEntity",
                table: "TaxDisplayNameEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxEntity",
                table: "TaxEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingSession_TaxId",
                schema: "Resources",
                table: "ChargingSession",
                column: "TaxId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChargingSession_TaxEntity_TaxId",
                schema: "Resources",
                table: "ChargingSession",
                column: "TaxId",
                principalTable: "TaxEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxDisplayNameEntity_TaxEntity_TaxId",
                table: "TaxDisplayNameEntity",
                column: "TaxId",
                principalTable: "TaxEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
