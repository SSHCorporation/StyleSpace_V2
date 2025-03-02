using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductService.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedsubcate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubCategory_Products_ProductId",
                table: "ProductSubCategory");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductSubCategory",
                newName: "ProductsId");

            migrationBuilder.AddColumn<Guid[]>(
                name: "SubCategoryIds",
                table: "Products",
                type: "uuid[]",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategory_Products_ProductsId",
                table: "ProductSubCategory",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubCategory_Products_ProductsId",
                table: "ProductSubCategory");

            migrationBuilder.DropColumn(
                name: "SubCategoryIds",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "ProductSubCategory",
                newName: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategory_Products_ProductId",
                table: "ProductSubCategory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
