namespace FrameworkFundamental.Numbers
{
    public class Numbers
    {
        public static void Run()
        {
            int hexValue = Convert.ToInt32("A", 16);
            Console.WriteLine(hexValue);

            // Math Class
            double power = Math.Pow(2, 8);
            Console.WriteLine(power);

            double squareRoot = Math.Sqrt(64);
            Console.WriteLine(squareRoot);

            double pi = Math.PI;
            Console.WriteLine(pi);

            double e = Math.E;
            Console.WriteLine(e);

            double max = Math.Max(4, 8);
            Console.WriteLine(max);

            double min = Math.Min(4, 8);
            Console.WriteLine(min);

            // Random Class
            Random random = new Random();
            int randomNumber = random.Next();
            Console.WriteLine(randomNumber);
        }
    }
}