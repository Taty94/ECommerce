using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.ECommerce.Infrastructure.Migrations
{
    public partial class AddBaseEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductBrand_ProductBrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ProductType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "ProductType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ProductBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductBrand",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "ProductBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductBrand_ProductBrandId",
                table: "Product",
                column: "ProductBrandId",
                principalTable: "ProductBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductBrand_ProductBrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ProductBrand");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductBrand");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "ProductBrand");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Product");

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
    }
}
