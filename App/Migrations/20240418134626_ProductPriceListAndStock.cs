using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simple_ecommerce_interview.Migrations
{
    /// <inheritdoc />
    public partial class ProductPriceListAndStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceList",
                table: "Products",
                newName: "PriceSale");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "PriceSale",
                table: "Products",
                newName: "PriceList");
        }
    }
}
