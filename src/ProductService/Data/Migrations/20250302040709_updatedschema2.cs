using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductService.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedschema2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubCategory_Products_ProductsId",
                table: "ProductSubCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubCategory_Subcategories_SubCategoriesId",
                table: "ProductSubCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subcategories",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubCategory",
                table: "ProductSubCategory");

            migrationBuilder.RenameTable(
                name: "Subcategories",
                newName: "SubCategories");

            migrationBuilder.RenameTable(
                name: "ProductSubCategory",
                newName: "ProductSubCategories");

            migrationBuilder.RenameColumn(
                name: "UpadatedBy",
                table: "SubCategories",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CraetedBy",
                table: "SubCategories",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Subcategories_CategoryId",
                table: "SubCategories",
                newName: "IX_SubCategories_CategoryId");

            migrationBuilder.RenameColumn(
                name: "UpadatedBy",
                table: "Products",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CraetedBy",
                table: "Products",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpadatedBy",
                table: "Categories",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CraetedBy",
                table: "Categories",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubCategory_SubCategoriesId",
                table: "ProductSubCategories",
                newName: "IX_ProductSubCategories_SubCategoriesId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SubCategories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategories",
                table: "SubCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubCategories",
                table: "ProductSubCategories",
                columns: new[] { "ProductsId", "SubCategoriesId" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategories_Products_ProductsId",
                table: "ProductSubCategories",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategories_SubCategories_SubCategoriesId",
                table: "ProductSubCategories",
                column: "SubCategoriesId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubCategories_Products_ProductsId",
                table: "ProductSubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubCategories_SubCategories_SubCategoriesId",
                table: "ProductSubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategories",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubCategories",
                table: "ProductSubCategories");

            migrationBuilder.RenameTable(
                name: "SubCategories",
                newName: "Subcategories");

            migrationBuilder.RenameTable(
                name: "ProductSubCategories",
                newName: "ProductSubCategory");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Subcategories",
                newName: "UpadatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Subcategories",
                newName: "CraetedBy");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategories_CategoryId",
                table: "Subcategories",
                newName: "IX_Subcategories_CategoryId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Products",
                newName: "UpadatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Products",
                newName: "CraetedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Categories",
                newName: "UpadatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Categories",
                newName: "CraetedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubCategories_SubCategoriesId",
                table: "ProductSubCategory",
                newName: "IX_ProductSubCategory_SubCategoriesId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subcategories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subcategories",
                table: "Subcategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubCategory",
                table: "ProductSubCategory",
                columns: new[] { "ProductsId", "SubCategoriesId" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategory_Products_ProductsId",
                table: "ProductSubCategory",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategory_Subcategories_SubCategoriesId",
                table: "ProductSubCategory",
                column: "SubCategoriesId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
