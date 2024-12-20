﻿using BibTrans.Areas.Identity.Data;
using BibTrans.Controllers;
using BibTrans.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BibTrans.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BorrowingsController : Controller
    {
        private readonly BibTransContext _context;

        public BorrowingsController(BibTransContext context)
        {
            _context = context;
        }

        // GET: Borrowings
        public async Task<IActionResult> Index()
        {
            var bibTransContext = _context.Borrowings.Include(b => b.Book).Include(b => b.User);
            return View(await bibTransContext.ToListAsync());
        }

        // GET: Borrowings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Borrowings == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowings
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // GET: Borrowings/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Borrowings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,BookId,BorrowDate,ReturnDate")] Borrowing borrowing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", borrowing.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", borrowing.UserId);
            return View(borrowing);
        }

        // GET: Borrowings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Borrowings == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowings.FindAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", borrowing.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", borrowing.UserId);
            return View(borrowing);
        }

        // POST: Borrowings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,BookId,BorrowDate,ReturnDate")] Borrowing borrowing)
        {
            if (id != borrowing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowingExists(borrowing.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", borrowing.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", borrowing.UserId);
            return View(borrowing);
        }

        // GET: Borrowings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Borrowings == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowings
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // POST: Borrowings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Borrowings == null)
            {
                return Problem("Entity set 'BibTransContext.Borrowings'  is null.");
            }
            var borrowing = await _context.Borrowings.FindAsync(id);
            if (borrowing != null)
            {
                _context.Borrowings.Remove(borrowing);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowingExists(int id)
        {
            return (_context.Borrowings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
