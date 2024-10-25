using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibTrans.Migrations
{
    public partial class UpdateBooks1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_BorrowedBY",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BorrowedBY",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "BorrowedBY",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "BorrowerId",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BorrowerId",
                table: "Books",
                column: "BorrowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_BorrowerId",
                table: "Books",
                column: "BorrowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_BorrowerId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BorrowerId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BorrowerId",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "BorrowedBY",
                table: "Books",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BorrowedBY",
                table: "Books",
                column: "BorrowedBY");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_BorrowedBY",
                table: "Books",
                column: "BorrowedBY",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
