using BibTrans.Areas.Identity.Data;
using BibTrans.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BibTrans.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BibTransContext _context;
        private readonly UserManager<BibTransUser> _userManager;

        public HomeController(ILogger<HomeController> logger, BibTransContext context, UserManager<BibTransUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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
            return Redirect("/Client");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}