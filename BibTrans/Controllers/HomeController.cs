using BibTrans.Areas.Identity.Data;
using BibTrans.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Diagnostics;

namespace BibTrans.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BibTransContext _context;
        private readonly UserManager<BibTransUser> _userManager;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, BibTransContext context, UserManager<BibTransUser> userManager, IEmailSender emailSender)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Books()
        {
            var booksList = await _context.Books.AsQueryable().Where(item => item.IsAvailable).ToListAsync();
            return View(booksList);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public async Task<IActionResult> Borrow(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Error();
            }

            if (id == null || _context.Books == null)
            {
                return BadRequest();
            }

            if (User.Identity?.IsAuthenticated == false)
            {
                return Redirect("/Identity/Account/Login");
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(User.Identity?.Name);

            var duplicate = await _context.Borrowings.AsQueryable().Where((item) => (item.UserId == user.Id) && (item.BookId == book.Id)).ToArrayAsync();
            if (duplicate.Length > 0)
            {
                return Conflict(new { message = "Książkę już wypożyczono" });
            }

            var newBorrowing = new Borrowing
            {
                BookId = book.Id,
                UserId = user.Id,
                BorrowDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddMonths(1)
            };

            await _context.Borrowings.AddAsync(newBorrowing);
            await _context.SaveChangesAsync();

            // Wysyłanie przypomnienia email o wypożyczeniu książki
            await SendReturnReminder(newBorrowing);

            return Redirect("/Client");
        }

        // Metoda wysyłająca przypomnienie email z terminem zwrotu
        private async Task SendReturnReminder(Borrowing borrowing)
        {
            var user = await _context.Users.FindAsync(borrowing.UserId);
            var book = await _context.Books.FindAsync(borrowing.BookId);

            if (user != null && book != null)
            {
                var subject = "Przypomnienie o terminie zwrotu książki";
                var htmlMessage = $"<p>Szanowny {user.UserName},</p>" +
                                  $"<p>Przypominamy o konieczności zwrotu książki <strong>{book.Title}</strong>.</p>" +
                                  $"<p>Data zwrotu: <strong>{borrowing.ReturnDate:dd MMMM yyyy}</strong>.</p>" +
                                  $"<p>Dziękujemy za skorzystanie z naszej biblioteki!</p>";

                // Logowanie informacji o wysyłaniu emaila
                Console.WriteLine($"Wysyłanie emaila do: {user.Email}, Tytuł książki: {book.Title}, Termin zwrotu: {borrowing.ReturnDate:dd MMMM yyyy}");

                // Wysyłanie emaila
                await _emailSender.SendEmailAsync(user.Email, subject, htmlMessage);

                // Potwierdzenie wysyłki
                Console.WriteLine("Email wysłany pomyślnie.");
            }
            else
            {
                Console.WriteLine("Nie udało się wysłać emaila - brak danych użytkownika lub książki.");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
