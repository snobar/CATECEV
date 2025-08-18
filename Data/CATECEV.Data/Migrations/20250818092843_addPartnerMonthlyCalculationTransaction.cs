using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CATECEV.Data.Migrations
{
    /// <inheritdoc />
    public partial class addPartnerMonthlyCalculationTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartnerMonthlyCalculationTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartnerId = table.Column<int>(type: "int", nullable: false),
                    MonthValue = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2(7)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    PartnerId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerMonthlyCalculationTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerMonthlyCalculationTransactions_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartnerMonthlyCalculationTransactions_Partner_PartnerId1",
                        column: x => x.PartnerId1,
                        principalTable: "Partner",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartnerMonthlyCalculationTransactions_PartnerId_MonthValue",
                table: "PartnerMonthlyCalculationTransactions",
                columns: new[] { "PartnerId", "MonthValue" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartnerMonthlyCalculationTransactions_PartnerId1",
                table: "PartnerMonthlyCalculationTransactions",
                column: "PartnerId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartnerMonthlyCalculationTransactions");
        }
    }
}
