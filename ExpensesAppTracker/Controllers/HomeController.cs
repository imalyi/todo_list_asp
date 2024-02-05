using ExpensesAppTracker.Data;
using ExpensesAppTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ExpensesAppTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpensesAppTrackerContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ExpensesAppTrackerContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        [Authorize(Roles = "Guest")]
        public IActionResult Index()
        {
            var totalMoneySpent = _dbContext.ExpenseItem.Sum(e => e.Price);

            var moneySpentPerDay = _dbContext.ExpenseItem
                .GroupBy(e => e.PurchaseDate.Date)
                .Select(group => new DailyReport
                {
                    Date = group.Key,
                    TotalMoneySpent = group.Sum(e => e.Price)
                })
                .ToList();

            var moneySpentPerMonth = _dbContext.ExpenseItem
                .GroupBy(e => new { e.PurchaseDate.Year, e.PurchaseDate.Month })
                .Select(group => new MonthlyReport
                {
                    Year = group.Key.Year,
                    Month = group.Key.Month,
                    TotalMoneySpent = group.Sum(e => e.Price)
                })
                .ToList();

            var viewModel = new ReportsViewModel
            {
                TotalMoneySpent = totalMoneySpent,
                MoneySpentPerDay = moneySpentPerDay,
                MoneySpentPerMonth = moneySpentPerMonth
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
