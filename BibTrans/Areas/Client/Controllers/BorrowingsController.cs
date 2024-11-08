using BibTrans.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibTrans.Areas.Client.Controllers
{
    [Area("Client")]
    public class BorrowingsController : Controller
    {
        private readonly BibTransContext _context;

        public BorrowingsController(BibTransContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bibTransContext = _context.Borrowings.Include(b => b.Book).Include(b => b.User);
            return View(await bibTransContext.ToListAsync());
        }
    }
}
