using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibTrans.Migrations
{
    public partial class Aktualizajacjabook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Autor",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autor",
                table: "Books");
        }
    }
}
