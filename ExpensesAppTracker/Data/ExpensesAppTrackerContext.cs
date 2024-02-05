using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExpensesTrackerApp.Models;

namespace ExpensesAppTracker.Data
{
    public class ExpensesAppTrackerContext : DbContext
    {
        public ExpensesAppTrackerContext (DbContextOptions<ExpensesAppTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<ExpensesTrackerApp.Models.ExpenseItem> ExpenseItem { get; set; } = default!;
        public DbSet<ExpensesTrackerApp.Models.Category> Category { get; set; } = default!;
        
        }
    }

