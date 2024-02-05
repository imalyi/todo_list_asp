using System.ComponentModel.DataAnnotations;

namespace ExpenseAppTracker.Models
{
    public class AccountModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}