using System;
using System.Collections.Generic;

namespace ExpensesAppTracker.Models
{
    public class ReportsViewModel
    {
        public decimal TotalMoneySpent { get; set; }
        public List<DailyReport> MoneySpentPerDay { get; set; }
        public List<MonthlyReport> MoneySpentPerMonth { get; set; }
    }

    public class DailyReport
    {
        public DateTime Date { get; set; }
        public decimal TotalMoneySpent { get; set; }
    }

    public class MonthlyReport
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalMoneySpent { get; set; }
    }
}
