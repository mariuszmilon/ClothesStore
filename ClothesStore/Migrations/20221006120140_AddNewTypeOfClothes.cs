using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesStore.Migrations
{
    public partial class AddNewTypeOfClothes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Decolletage",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Hood",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Neckline",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shoe_Type",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Decolletage",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Hood",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Neckline",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Shoe_Type",
                table: "Items");
        }
    }
}
