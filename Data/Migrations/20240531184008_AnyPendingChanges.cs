using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agri_Energy_Connect_Platform.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnyPendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "FarmersId1",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_FarmersId1",
                table: "Products",
                column: "FarmersId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Farmers_FarmersId1",
                table: "Products",
                column: "FarmersId1",
                principalTable: "Farmers",
                principalColumn: "FarmersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Farmers_FarmersId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FarmersId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FarmersId1",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
