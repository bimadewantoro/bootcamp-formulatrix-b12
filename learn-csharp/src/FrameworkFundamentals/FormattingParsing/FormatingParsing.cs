using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace FrameworkFundamental.FormatingParsing
{
    public class FormatingParsing
    {
        public static void Run()
        {
            // ToString and Parse
            string s = true.ToString();
            Console.WriteLine(s);

            bool b = bool.Parse("True");
            Console.WriteLine(b);

            // Format provider. Change currency symbol
            NumberFormatInfo formatInfo = new NumberFormatInfo();
            formatInfo.CurrencySymbol = "Rp";
            Console.WriteLine(12000.ToString("C", formatInfo));

            // NumberFormatInfo and DateTimeFormatInfo
            var dateTime = DateTime.Now;
            var number = 123456.789;
            var culture = CultureInfo.CurrentCulture;

            Console.WriteLine(number.ToString("C", culture));
            Console.WriteLine(dateTime.ToString("d", culture));

            // Composite Formatting
            string composite = string.Format("It is {0:yyyy} year. Today is {0:dddd}!", dateTime);
            Console.WriteLine(composite);
        }
    }
}