using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibTrans.Migrations
{
    public partial class AddBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if the Books table already exists
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Books]') AND type in (N'U'))
                BEGIN
                    CREATE TABLE [Books] (
                        [Id] int NOT NULL IDENTITY,
                        [Title] nvarchar(max) NOT NULL,
                        [ISBN] nvarchar(max) NOT NULL,
                        [IsAvailable] bit NOT NULL,
                        [Description] nvarchar(max) NOT NULL,
                        [BorrowedBY] nvarchar(max) NULL,
                        CONSTRAINT [PK_Books] PRIMARY KEY ([Id])
                    );
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
