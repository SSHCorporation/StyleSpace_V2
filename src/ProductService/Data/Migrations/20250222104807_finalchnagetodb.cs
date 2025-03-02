using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductService.Data.Migrations
{
    /// <inheritdoc />
    public partial class finalchnagetodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_Products_ProductId",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_Subcategories_ProductId",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "SubCategoryIds",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductSubCategory",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubCategoriesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubCategory", x => new { x.ProductId, x.SubCategoriesId });
                    table.ForeignKey(
                        name: "FK_ProductSubCategory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSubCategory_Subcategories_SubCategoriesId",
                        column: x => x.SubCategoriesId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubCategory_SubCategoriesId",
                table: "ProductSubCategory",
                column: "SubCategoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSubCategory");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Subcategories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<List<Guid>>(
                name: "SubCategoryIds",
                table: "Products",
                type: "uuid[]",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_ProductId",
                table: "Subcategories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_Products_ProductId",
                table: "Subcategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
