using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpensesTrackerApp.Helpers
{
    public static class DisplayCategoryNameHelper
    {
        public static IHtmlContent DisplayCategoryName(this IHtmlHelper html, string categoryName)
        {
            string colorClass = GetColorClass(categoryName);

            var result = $"<span class='{colorClass}'>{categoryName}</span>";
            return new HtmlString(result);
        }

        private static string GetColorClass(string categoryName)
        {
            if (!string.IsNullOrEmpty(categoryName) && categoryName.StartsWith("G", System.StringComparison.OrdinalIgnoreCase))
            {
                return "text-success"; // Зелёный цвет текста для категорий, начинающихся с "G"
            }
            return "text-dark"; // Черный цвет текста по умолчанию
        }
    }
}
