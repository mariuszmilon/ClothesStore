using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesStore.Migrations
{
    public partial class AddBrandToItemTabele : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Items");
        }
    }
}
