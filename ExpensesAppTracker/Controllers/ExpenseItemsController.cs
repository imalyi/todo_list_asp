using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpensesAppTracker.Data;
using ExpensesTrackerApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExpensesAppTracker.Controllers
{
    public class ExpenseItemsController : Controller
    {
        private readonly ExpensesAppTrackerContext _context;

        public ExpenseItemsController(ExpensesAppTrackerContext context)
        {
            _context = context;
        }

        // GET: ExpenseItems
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            var expensesAppTrackerContext = _context.ExpenseItem.Include(e => e.Category);
            return View(await expensesAppTrackerContext.ToListAsync());
        }

        // GET: ExpenseItems/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseItem = await _context.ExpenseItem
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseItem == null)
            {
                return NotFound();
            }

            return View(expenseItem);
        }

        // GET: ExpenseItems/Create
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: ExpenseItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,PurchaseDate,CategoryId")] ExpenseItem expenseItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenseItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", expenseItem.CategoryId);
            return View(expenseItem);
        }

        // GET: ExpenseItems/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseItem = await _context.ExpenseItem.FindAsync(id);
            if (expenseItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", expenseItem.CategoryId);
            return View(expenseItem);
        }

        // POST: ExpenseItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,PurchaseDate,CategoryId")] ExpenseItem expenseItem)
        {
            Console.WriteLine(ModelState.IsValid);
            if (id != expenseItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenseItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseItemExists(expenseItem.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", expenseItem.CategoryId);
            return View(expenseItem);
        }

        // GET: ExpenseItems/Delete/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseItem = await _context.ExpenseItem
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseItem == null)
            {
                return NotFound();
            }

            return View(expenseItem);
        }

        // POST: ExpenseItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expenseItem = await _context.ExpenseItem.FindAsync(id);
            if (expenseItem != null)
            {
                _context.ExpenseItem.Remove(expenseItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseItemExists(int id)
        {
            return _context.ExpenseItem.Any(e => e.Id == id);
        }
    }
}
