using System.ComponentModel.DataAnnotations;

namespace ExpensesTrackerApp.Models
{
    public class Category
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, ErrorMessage = "Category name can have up to 50 characters.")]
        [Display(Name = "Category Name")]
        [UniqueCategoryName]
        public string Name { get; set; }
        public List<ExpenseItem> Expenses { get; set; } = new List<ExpenseItem>();
    }
}
