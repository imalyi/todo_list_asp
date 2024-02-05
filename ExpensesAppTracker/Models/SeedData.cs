using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExpensesTrackerApp.Models;
using ExpensesAppTracker.Data;

namespace ExpensesTrackerApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ExpensesAppTrackerContext(
                serviceProvider.GetRequiredService<DbContextOptions<ExpensesAppTrackerContext>>()))
            {
                // Look for any existing data.
                if (context.Category.Any() || context.ExpenseItem.Any())
                {
                    return; // Database has been seeded
                }

                // Seed Categories
                var categories = new Category[]
                {
                    new Category { Name = "Groceries" },
                    new Category { Name = "Utilities" },
                    new Category { Name = "Entertainment" },
                    // Add more categories as needed
                };
                context.Category.AddRange(categories);
                context.SaveChanges();

                // Seed ExpenseItems
                var expenseItems = new ExpenseItem[]
                {
                    new ExpenseItem
                    {
                        Name = "Grocery Shopping",
                        Price = 50.0m,
                        Description = "Monthly grocery shopping",
                        PurchaseDate = new DateTime(2022, 1, 15),
                        CategoryId = categories[0].Id // Corresponds to "Groceries" category
                    },
                    new ExpenseItem
                    {
                        Name = "Electricity Bill",
                        Price = 80.0m,
                        Description = "Electricity bill for the month",
                        PurchaseDate = new DateTime(2022, 1, 20),
                        CategoryId = categories[1].Id // Corresponds to "Utilities" category
                    },
                    // Add more expense items as needed
                };
                context.ExpenseItem.AddRange(expenseItems);
                context.SaveChanges();
            }
        }
    }
}
