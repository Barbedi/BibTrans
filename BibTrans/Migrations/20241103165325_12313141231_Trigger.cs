using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibTrans.Migrations
{
    public partial class _12313141231_Trigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TRIGGER trg_AfterUserRegistration
                ON dbo.AspNetUsers
                AFTER INSERT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    DECLARE @UserId NVARCHAR(450);
                    DECLARE @Action NVARCHAR(MAX) = 'Rejestracja';
                    DECLARE @Controller NVARCHAR(MAX) = 'Konto'; -- lub inna nazwa kontrolera
                    DECLARE @TimeStamp DATETIME2(7) = SYSDATETIME();

                    SELECT @UserId = Id FROM inserted; -- Pobranie Id nowego użytkownika

                    -- Wstawienie logu aktywności do tabeli ActivityLogs
                    INSERT INTO dbo.ActivityLogs (UserId, Action, Controller, TimeStamp)
                    VALUES (@UserId, @Action, @Controller, @TimeStamp);
                END
            ");
            migrationBuilder.Sql(@" 
                CREATE TRIGGER trg_AfterUserLogin 
ON dbo.AspNetUsers 
AFTER UPDATE 
AS
BEGIN
    SET NOCOUNT ON; 

    DECLARE @UserId NVARCHAR(450);
    DECLARE @Action NVARCHAR(MAX) = 'Użytkownik zalogowany';
    DECLARE @Controller NVARCHAR(MAX) = 'Konto'; 
    DECLARE @TimeStamp DATETIME2(7) = SYSDATETIME(); 

    SELECT @UserId = Id FROM inserted; 


    INSERT INTO dbo.ActivityLogs (UserId, Action, Controller, TimeStamp)
    VALUES (@UserId, @Action, @Controller, @TimeStamp); 
END;
");
            migrationBuilder.Sql(@"
CREATE TRIGGER trg_AfterBookInsert
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
CREATE TRIGGER trg_AfterBookDelete
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
CREATE TRIGGER trg_AfterBookUpdate
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
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_AfterUserRegistration ON dbo.AspNetUsers");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_AfterUserLogin ON dbo.AspNetUsers");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_AfterBookInsert ON dbo.Books");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_AfterBookDelete ON dbo.Books");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_AfterBookUpdate ON dbo.Books");

        }
    }
}
