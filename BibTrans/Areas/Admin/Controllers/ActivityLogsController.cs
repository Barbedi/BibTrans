﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibTrans.Areas.Identity.Data;
using BibTrans.Models;
using X.PagedList;
using X.PagedList.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace BibTrans.Area.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ActivityLogsController:Controller
    {
        private readonly BibTransContext _context;
        public ActivityLogsController(BibTransContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int ?page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var ActiveList= await _context.ActivityLogs.OrderByDescending(x => x.Id).ToListAsync();

            var ActiveListPaged = ActiveList.ToPagedList(pageNumber, pageSize);
            return View(ActiveListPaged);
        }
    }
}