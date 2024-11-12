using BibTrans.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibTrans.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize]
    public class BorrowingsController : Controller
    {
        private readonly BibTransContext _context;
        private readonly UserManager<BibTransUser> _userManager;

        public BorrowingsController(BibTransContext context, UserManager<BibTransUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var bibTransContext = _context.Borrowings.Include(b => b.Book).Include(b => b.User)
                .Where(b => b.UserId == _userManager.GetUserId(User));
            return View(await bibTransContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var parsedId = int.Parse(id);
            var borrowing = await _context.Borrowings.FindAsync(parsedId);
            if (borrowing == null)
            {
                return NotFound();
            }

            _context.Borrowings.Remove(borrowing);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
