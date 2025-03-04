using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CATECEV.Data.Migrations
{
    /// <inheritdoc />
    public partial class addlookups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Shared");

            migrationBuilder.CreateTable(
                name: "LookupCategory",
                schema: "Shared",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lookups",
                schema: "Shared",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArabicDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LookupCategoryId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lookups_LookupCategory_LookupCategoryId",
                        column: x => x.LookupCategoryId,
                        principalSchema: "Shared",
                        principalTable: "LookupCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Shared",
                table: "LookupCategory",
                columns: new[] { "Id", "Description", "IsActive" },
                values: new object[] { 1, "Payment Status", true });

            migrationBuilder.InsertData(
                schema: "Shared",
                table: "Lookups",
                columns: new[] { "Id", "ArabicDescription", "EnglishDescription", "IsActive", "LookupCategoryId", "OrderId" },
                values: new object[] { 1, "مدفوع", "Paid", true, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Lookups_LookupCategoryId",
                schema: "Shared",
                table: "Lookups",
                column: "LookupCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lookups",
                schema: "Shared");

            migrationBuilder.DropTable(
                name: "LookupCategory",
                schema: "Shared");
        }
    }
}
