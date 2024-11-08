using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibTrans.Migrations
{
    public partial class _12313141231_TriggerBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE OR ALTER TRIGGER trg_AfterBookInsert
ON dbo.Books
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @BookId INT;
    DECLARE @Action NVARCHAR(MAX) = 'Książka dodana'; 
    DECLARE @TimeStamp DATETIME2(7) = SYSDATETIME(); 

    SELECT @BookId = Id FROM inserted; 

    
    INSERT INTO dbo.ActivityLogs (UserId, Action, Controller, TimeStamp)
    VALUES (NULL, @Action, 'Books', @TimeStamp); 
END;

");

            migrationBuilder.Sql(@"
CREATE OR ALTER TRIGGER trg_AfterBookDelete
ON dbo.Books
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @BookId INT;
    DECLARE @Action NVARCHAR(MAX) = 'Książka usunięta'; 
    DECLARE @TimeStamp DATETIME2(7) = SYSDATETIME(); 

    SELECT @BookId = Id FROM deleted;

    
    INSERT INTO dbo.ActivityLogs (UserId, Action, Controller, TimeStamp)
    VALUES (NULL, @Action, 'Books', @TimeStamp); 
END;
");
            migrationBuilder.Sql(@"
CREATE OR ALTER TRIGGER trg_AfterBookUpdate
ON dbo.Books
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @BookId INT;
    DECLARE @Action NVARCHAR(MAX) = 'Książka edytowana'; 
    DECLARE @TimeStamp DATETIME2(7) = SYSDATETIME(); 

    SELECT @BookId = Id FROM inserted; 

    INSERT INTO dbo.ActivityLogs (UserId, Action, Controller, TimeStamp)
    VALUES (NULL, @Action, 'Books', @TimeStamp); 
END;
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_AfterBookInsert ON dbo.Books");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_AfterBookDelete ON dbo.Books");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_AfterBookUpdate ON dbo.Books");

        }
    }
}
