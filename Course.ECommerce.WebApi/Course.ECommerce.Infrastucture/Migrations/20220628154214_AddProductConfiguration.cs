using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.ECommerce.Infrastructure.Migrations
{
    public partial class AddProductConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Product",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductBrandId",
                table: "Product",
                type: "nvarchar(4)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeId",
                table: "Product",
                type: "nvarchar(4)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductBrandId",
                table: "Product",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductBrand_ProductBrandId",
                table: "Product",
                column: "ProductBrandId",
                principalTable: "ProductBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductBrand_ProductBrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductBrandId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductBrandId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Product");
        }
    }
}
