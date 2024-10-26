using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibTrans.Migrations
{
    public partial class AddBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
        name: "Books",
        columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
            Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
            IsAvailable = table.Column<bool>(type: "bit", nullable: false),
            Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            BorrowedBY = table.Column<string>(type: "nvarchar(max)", nullable: true) // Optional
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Books", x => x.Id);
        });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
