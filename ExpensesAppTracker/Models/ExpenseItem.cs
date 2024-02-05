using System.ComponentModel.DataAnnotations;

namespace ExpensesTrackerApp.Models
{
    public class ExpenseItem
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can have up to 100 characters.")]
        [Display(Name = "Expense name")]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [StringLength(100, ErrorMessage = "Description can have up to 100 characters.")]
        [Display(Name = "Expense description")]
        public string? Description { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Purchase date is required.")]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }
        [Required(ErrorMessage = "Category ID is required.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}