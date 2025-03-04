using System.Security.Cryptography.X509Certificates;

namespace FrameworkFundamental.Enums
{
    public class Enums
    {
        public enum Days
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }

        public static void Run()
        {
            Days today = Days.Monday;
            Console.WriteLine(today);

            int dayNumber = (int)today;
            Console.WriteLine(dayNumber);

            Days day = (Days)4;
            Console.WriteLine(day);

            Days day2 = (Days)Enum.Parse(typeof(Days), "Wednesday");
            Console.WriteLine(day2);
        }
    }
}