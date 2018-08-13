using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class AddedInventories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    InventoryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CrossReference = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DistributionUnit = table.Column<int>(nullable: false),
                    InventoryType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    ReorderPoint = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.InventoryId);
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "InventoryId", "CrossReference", "Description", "DistributionUnit", "InventoryType", "Name", "Price", "Quantity", "Rank", "ReorderPoint" },
                values: new object[] { 1L, "Test", "fwefwe", 0, 0, "cdscsdc", 5.25m, 2.0, 1, 3.0 });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "InventoryId", "CrossReference", "Description", "DistributionUnit", "InventoryType", "Name", "Price", "Quantity", "Rank", "ReorderPoint" },
                values: new object[] { 2L, "vdfvd", "bdbd", 1, 1, "vdfvf", 7.34m, 6.0, 2, 11.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");
        }
    }
}
