using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tender_Search.Data;
using Tender_Search.Models;

namespace Tender_Search.Controllers
{
    public class TendersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TendersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tenders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tender.ToListAsync());
        }

        // GET: Tenders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tender = await _context.Tender
                .FirstOrDefaultAsync(m => m.ReferenceNumber == id);
            if (tender == null)
            {
                return NotFound();
            }

            return View(tender);
        }

        // GET: Tenders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tenders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenderName,ReferenceNumber,ReleaseDate,CloseDate,Description")] Tender tender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tender);
        }

        // GET: Tenders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tender = await _context.Tender.FindAsync(id);
            if (tender == null)
            {
                return NotFound();
            }
            return View(tender);
        }

        // POST: Tenders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TenderName,ReferenceNumber,ReleaseDate,CloseDate,Description")] Tender tender)
        {
            if (id != tender.ReferenceNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenderExists(tender.ReferenceNumber))
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
            return View(tender);
        }

        // GET: Tenders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tender = await _context.Tender
                .FirstOrDefaultAsync(m => m.ReferenceNumber == id);
            if (tender == null)
            {
                return NotFound();
            }

            return View(tender);
        }

        // POST: Tenders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tender = await _context.Tender.FindAsync(id);
            _context.Tender.Remove(tender);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenderExists(int id)
        {
            return _context.Tender.Any(e => e.ReferenceNumber == id);
        }
    }
}
