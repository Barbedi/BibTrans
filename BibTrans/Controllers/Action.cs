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
    public class Action:Controller
    {
        private readonly BibTransContext _context;
        public Action(BibTransContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var ActiveList = await _context.ActivityLogs.ToListAsync();
            return View(ActiveList);
        }
    }
}