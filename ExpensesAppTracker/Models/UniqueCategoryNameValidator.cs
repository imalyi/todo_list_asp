using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpensesTrackerApp.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class UniqueCategoryNameAttribute : ValidationAttribute
    {
        private readonly List<string> uniqueNames = new List<string>();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string categoryName = value.ToString();

                // Проверяем, есть ли другая категория с таким же именем в коллекции
                if (uniqueNames.Contains(categoryName))
                {
                    return new ValidationResult("Category name must be unique.");
                }

                // Добавляем уникальное имя категории в коллекцию
                uniqueNames.Add(categoryName);
            }

            return ValidationResult.Success;
        }
    }
}
