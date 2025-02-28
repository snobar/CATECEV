using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hijazi.Migrations
{
    /// <inheritdoc />
    public partial class addTopUpAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TopUpAmount",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopUpAmount",
                table: "Company");
        }
    }
}
