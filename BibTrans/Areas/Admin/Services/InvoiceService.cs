using BibTrans.Areas.Identity.Data;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System.Linq;

namespace BibTrans.Areas.Admin.Services
{
    public class InvoiceService
    {
        private readonly BibTransContext _context;

        public InvoiceService(BibTransContext context)
        {
            _context = context;
        }

        public PdfDocument GetInvoice()
        {
            var document = new Document();
            BuildDocument(document);

            var pdfRenderer = new PdfDocumentRenderer();
            pdfRenderer.Document = document;

            pdfRenderer.RenderDocument();
            return pdfRenderer.PdfDocument;
        }

        private void BuildDocument(Document document)
        {
            Section section = document.AddSection();

            // Nagłówek
            var paragraph = section.AddParagraph();
            paragraph.AddText("BibTrans - System do Zarzadzania Biblioteka ");
            paragraph.AddLineBreak();
            paragraph.AddText("Strona internetowa: www.bibtrans.pl");
            paragraph.AddLineBreak();
            paragraph.AddText("Email: bibtransio@gmail.com ");
            paragraph.AddLineBreak();
            paragraph.AddText("Telefon: 123 456 789");
            paragraph.Format.SpaceAfter = 20;

            // Tytuł raportu
            paragraph = section.AddParagraph();
            int liczbaRaportu = 1;
            paragraph.AddText($"Raport stanu biblioteki nr {liczbaRaportu}");
            paragraph.Format.Font.Size = 20;
            paragraph.Format.Font.Bold = true;
            paragraph.AddLineBreak();
            string currentDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            paragraph.AddText("Data wygenerowania: " + currentDate);
            paragraph.Format.SpaceAfter = 20;

            // Pobranie książek z bazy danych
            var books = _context.Books.ToList();

            // Tabela książek
            var table = section.AddTable();
            table.Borders.Width = 0.75;
            table.AddColumn("1cm");
            table.AddColumn("5cm");
            table.AddColumn("5cm");
            table.AddColumn("3cm");

            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Font.Bold = true;
            row.Cells[0].AddParagraph("Lp.");
            row.Cells[1].AddParagraph("Tytuł");
            row.Cells[2].AddParagraph("Autor");
            row.Cells[3].AddParagraph("Dostępność");

            // Wypełnienie tabeli danymi z bazy
            for (int i = 0; i < books.Count; i++)
            {
                var book = books[i];
                row = table.AddRow();
                row.Cells[0].AddParagraph((i + 1).ToString()); // Numeracja
                row.Cells[1].AddParagraph(book.Title);
                row.Cells[2].AddParagraph(book.Autor);
                row.Cells[3].AddParagraph(book.IsAvailable ? "Tak" : "Nie");
            }

            // Stopka
            paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("BibTrans - System do Zarzadzania Biblioteka · Polska · Wszelkie prawa zastrzeżone ");
            paragraph.Format.Alignment = ParagraphAlignment.Center;
        }
    }
}
