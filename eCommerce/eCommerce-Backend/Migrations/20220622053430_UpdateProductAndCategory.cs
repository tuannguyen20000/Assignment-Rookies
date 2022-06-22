using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce_Backend.Migrations
{
    public partial class UpdateProductAndCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoiesId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoiesId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoiesId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "ProductInCategory",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInCategory", x => new { x.CategoriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductInCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInCategory_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCategory_ProductsId",
                table: "ProductInCategory",
                column: "ProductsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInCategory");

            migrationBuilder.AddColumn<int>(
                name: "CategoiesId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoiesId",
                table: "Products",
                column: "CategoiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoiesId",
                table: "Products",
                column: "CategoiesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
