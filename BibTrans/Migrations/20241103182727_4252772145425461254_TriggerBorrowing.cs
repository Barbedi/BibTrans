using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibTrans.Migrations
{
    public partial class _4252772145425461254_TriggerBorrowing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE TRIGGER trg_AfterBookBorrow
            ON dbo.Borrowings
            AFTER INSERT
            AS
            BEGIN
                SET NOCOUNT ON;

                DECLARE @UserId NVARCHAR(450);
                DECLARE @BookId INT;
                DECLARE @Action NVARCHAR(MAX) = 'Książka wypożyczona'; 
                DECLARE @Controller NVARCHAR(MAX) = 'Borrowings'; 
                DECLARE @TimeStamp DATETIME2(7) = SYSDATETIME();
                DECLARE @Description NVARCHAR(MAX);

                SELECT @UserId = UserId, @BookId = BookId FROM inserted; 

                SET @Description = 'Książka o ID ' + CAST(@BookId AS NVARCHAR(10)) + ' została wypożyczona przez użytkownika o ID ' + @UserId;

                INSERT INTO dbo.ActivityLogs (UserId, Action, Controller, TimeStamp, Description)
                VALUES (@UserId, @Action, @Controller, @TimeStamp, @Description); 
            END;
            ");

            migrationBuilder.Sql(@"
            CREATE TRIGGER trg_AfterBookReturn
            ON dbo.Borrowings
            AFTER DELETE
            AS
            BEGIN
                SET NOCOUNT ON;

                DECLARE @UserId NVARCHAR(450);
                DECLARE @BookId INT;
                DECLARE @Action NVARCHAR(MAX) = 'Wypożyczenie anulowane'; 
                DECLARE @Controller NVARCHAR(MAX) = 'Borrowings'; 
                DECLARE @TimeStamp DATETIME2(7) = SYSDATETIME();
                DECLARE @Description NVARCHAR(MAX);

                SELECT @UserId = UserId, @BookId = BookId FROM deleted; 

                SET @Description = 'Wypożyczenie książki o ID ' + CAST(@BookId AS NVARCHAR(10)) + ' zostało anulowane przez użytkownika o ID ' + @UserId;

                INSERT INTO dbo.ActivityLogs (UserId, Action, Controller, TimeStamp, Description)
                VALUES (@UserId, @Action, @Controller, @TimeStamp, @Description); 
            END;
            ");
            migrationBuilder.Sql(@"CREATE TRIGGER trg_AfterBookUpdate1
ON dbo.Borrowings
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserId NVARCHAR(450);
    DECLARE @BookId INT;
    DECLARE @Action NVARCHAR(MAX) = 'Wypożyczenie edytowane'; 
    DECLARE @Controller NVARCHAR(MAX) = 'Borrowings'; 
    DECLARE @TimeStamp DATETIME2(7) = SYSDATETIME();
    DECLARE @Description NVARCHAR(MAX);

    SELECT @UserId = UserId, @BookId = BookId FROM inserted; 

    SET @Description = 'Wypożyczenie książki o ID ' + CAST(@BookId AS NVARCHAR(10)) + ' zostało edytowane przez użytkownika o ID ' + @UserId;

    INSERT INTO dbo.ActivityLogs (UserId, Action, Controller, TimeStamp, Description)
    VALUES (@UserId, @Action, @Controller, @TimeStamp, @Description); 
END;");
        

    }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER trg_AfterBookBorrow ON dbo.Borrowings;");
            migrationBuilder.Sql("DROP TRIGGER trg_AfterBookReturn ON dbo.Borrowings;");
            migrationBuilder.Sql("DROP TRIGGER trg_AfterBookUpdate1 ON dbo.Borrowings;");

        }
    }
}
