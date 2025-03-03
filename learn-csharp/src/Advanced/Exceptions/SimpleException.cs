namespace Advanced.SimpleException
{
    public class SimpleException
    {
        public static void Run()
        {
            try
            {
                var a = 5;
                var b = 0;
                Console.WriteLine(a / b);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Finally block is always executed.");
            }
        }
    }
}