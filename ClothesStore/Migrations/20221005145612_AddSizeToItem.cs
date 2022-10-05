using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesStore.Migrations
{
    public partial class AddSizeToItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Items");
        }
    }
}
