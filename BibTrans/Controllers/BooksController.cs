using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibTrans.Areas.Identity.Data;
using BibTrans.Models;

namespace BibTrans.Controllers
{
    public class BooksController : Controller
    {
        private readonly BibTransContext _context;

        public BooksController(BibTransContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var bibTransContext = _context.Books.Include(b => b.Borrower);
            return View(await bibTransContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .Include(b => b.Borrower)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["BorrowedBY"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ISBN,IsAvailable,Description,BorrowedBY")] Books books)
        {
            if (ModelState.IsValid)
            {
                _context.Add(books);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BorrowedBY"] = new SelectList(_context.Users, "Id", "Id", books.BorrowedBY);
            return View(books);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var books = await _context.Books.FindAsync(id);
            if (books == null)
            {
                return NotFound();
            }
            ViewData["BorrowedBY"] = new SelectList(_context.Users, "Id", "Id", books.BorrowedBY);
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ISBN,IsAvailable,Description,BorrowedBY")] Books books)
        {
            if (id != books.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(books);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(books.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BorrowedBY"] = new SelectList(_context.Users, "Id", "Id", books.BorrowedBY);
            return View(books);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .Include(b => b.Borrower)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'BibTransContext.Books'  is null.");
            }
            var books = await _context.Books.FindAsync(id);
            if (books != null)
            {
                _context.Books.Remove(books);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksExists(int id)
        {
          return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
