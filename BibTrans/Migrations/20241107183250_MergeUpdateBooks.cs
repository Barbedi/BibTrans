using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibTrans.Migrations
{
    public partial class MergeUpdateBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF OBJECT_ID('FK_Books_AspNetUsers_BorrowedBY', 'F') IS NOT NULL
                BEGIN
                    ALTER TABLE [Books] DROP CONSTRAINT [FK_Books_AspNetUsers_BorrowedBY];
                END
            ");

            migrationBuilder.Sql(@"
                IF OBJECT_ID('FK_Books_AspNetUsers_BorrowerId', 'F') IS NOT NULL
                BEGIN
                    ALTER TABLE [Books] DROP CONSTRAINT [FK_Books_AspNetUsers_BorrowerId];
                END
            ");

            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Books_BorrowedBY' AND object_id = OBJECT_ID('Books'))
                BEGIN
                    DROP INDEX [IX_Books_BorrowedBY] ON [Books];
                END
            ");

            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Books_BorrowerId' AND object_id = OBJECT_ID('Books'))
                BEGIN
                    DROP INDEX [IX_Books_BorrowerId] ON [Books];
                END
            ");

            migrationBuilder.Sql(@"
                IF COL_LENGTH('Books', 'BorrowedBY') IS NOT NULL
                BEGIN
                    DECLARE @var0 sysname;
                    SELECT @var0 = [d].[name]
                    FROM [sys].[default_constraints] [d]
                    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
                    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'BorrowedBY');
                    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var0 + '];');
                    ALTER TABLE [Books] ALTER COLUMN [BorrowedBY] nvarchar(max) NOT NULL;
                END
            ");

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

            migrationBuilder.Sql(@"
                IF COL_LENGTH('Books', 'BorrowedBY') IS NOT NULL
                BEGIN
                    ALTER TABLE [Books] ALTER COLUMN [BorrowedBY] nvarchar(450) NOT NULL;
                    CREATE INDEX [IX_Books_BorrowedBY] ON [Books]([BorrowedBY]);
                    ALTER TABLE [Books] ADD CONSTRAINT [FK_Books_AspNetUsers_BorrowedBY] FOREIGN KEY ([BorrowedBY]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE;
                END
            ");
        }
    }
}