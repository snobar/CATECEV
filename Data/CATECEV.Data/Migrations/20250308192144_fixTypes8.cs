using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CATECEV.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixTypes8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargingSession_ChargePoint_ChargePointId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "Resources",
                table: "ChargingSession",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TaxId",
                schema: "Resources",
                table: "ChargingSession",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EvseId",
                schema: "Resources",
                table: "ChargingSession",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ChargePointId",
                schema: "Resources",
                table: "ChargingSession",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AMPECOChargePointId",
                schema: "Resources",
                table: "ChargingSession",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AMPECOConnectorId",
                schema: "Resources",
                table: "ChargingSession",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AMPECOEvseId",
                schema: "Resources",
                table: "ChargingSession",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AMPECOTaxId",
                schema: "Resources",
                table: "ChargingSession",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AMPECOUserId",
                schema: "Resources",
                table: "ChargingSession",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ChargingSession_ChargePoint_ChargePointId",
                schema: "Resources",
                table: "ChargingSession",
                column: "ChargePointId",
                principalSchema: "Resources",
                principalTable: "ChargePoint",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargingSession_ChargePoint_ChargePointId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.DropColumn(
                name: "AMPECOChargePointId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.DropColumn(
                name: "AMPECOConnectorId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.DropColumn(
                name: "AMPECOEvseId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.DropColumn(
                name: "AMPECOTaxId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.DropColumn(
                name: "AMPECOUserId",
                schema: "Resources",
                table: "ChargingSession");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "Resources",
                table: "ChargingSession",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TaxId",
                schema: "Resources",
                table: "ChargingSession",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EvseId",
                schema: "Resources",
                table: "ChargingSession",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChargePointId",
                schema: "Resources",
                table: "ChargingSession",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChargingSession_ChargePoint_ChargePointId",
                schema: "Resources",
                table: "ChargingSession",
                column: "ChargePointId",
                principalSchema: "Resources",
                principalTable: "ChargePoint",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
