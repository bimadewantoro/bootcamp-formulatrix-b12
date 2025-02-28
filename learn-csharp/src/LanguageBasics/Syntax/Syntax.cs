namespace LearnCSharp
{
    public class Syntax
    {
        public static void Run()
        {
            // Declare variables
            int x = 0;
            int y = 0;
            int z = 0;

            // Assign values
            x = 1;
            y = 2;
            z = x + y;

            // Print result
            Console.WriteLine(z);

            // Declare and assign in one line
            int a = 1;
            int b = 2;
            int c = a + b;

            // Print result
            Console.WriteLine(c);

            // Declare and assign multiple variables in one line
            int d = 1, e = 2, f = d + e;

            // Print result
            Console.WriteLine(f);
        }
    }
}