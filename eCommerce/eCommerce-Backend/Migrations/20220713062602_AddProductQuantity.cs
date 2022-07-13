using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce_Backend.Migrations
{
    public partial class AddProductQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "Price", "ProductName", "ProductQuantity", "Status", "UpdatedDate" },
                values: new object[] { 343434, new DateTime(2022, 7, 13, 0, 0, 0, 0, DateTimeKind.Local), "Des 1", 12313m, "Product name 1", 1, 0, new DateTime(2022, 7, 13, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 343434);

            migrationBuilder.DropColumn(
                name: "ProductQuantity",
                table: "Products");
        }
    }
}
