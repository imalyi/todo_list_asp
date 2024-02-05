using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace ExpensesTrackerApp.Helpers
{
    public static class FormatPriceHelper
    {
        public static IHtmlContent FormatPrice(this IHtmlHelper html, decimal price)
        {
            string formattedPrice = FormatPrice(price);

            var result = $"<span>{formattedPrice}</span>";
            return new HtmlString(result);
        }

        private static string FormatPrice(decimal price)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US"); // Используем форматирование для США
            string formattedPrice = string.Format(cultureInfo, "{0:C}", price); // Форматирование в виде валюты

            return formattedPrice;
        }
    }
}
