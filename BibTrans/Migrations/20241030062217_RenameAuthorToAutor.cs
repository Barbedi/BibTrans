using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibTrans.Migrations
{
    public partial class RenameAuthorToAutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Books",
                newName: "Autor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Autor",
                table: "Books",
                newName: "Author");
        }
    }
}
