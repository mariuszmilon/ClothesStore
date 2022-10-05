using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesStore.Migrations
{
    public partial class SomeChangesWithNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Items",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Items",
                newName: "Name");
        }
    }
}
