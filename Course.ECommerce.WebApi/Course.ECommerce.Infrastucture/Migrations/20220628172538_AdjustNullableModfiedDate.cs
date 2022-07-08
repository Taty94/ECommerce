using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.ECommerce.Infrastructure.Migrations
{
    public partial class AdjustNullableModfiedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "ProductBrand");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Product");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ProductType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ProductBrand",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Product",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ProductBrand");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Product");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "ProductType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "ProductBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
