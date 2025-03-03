namespace Advanced.MultipleException
{
    public class MultipleException
    {
        public static void Run()
        {
            try
            {
                var filePath = "file.txt";
                var content = File.ReadAllText(filePath);
                Console.WriteLine(content);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("The file was not found: " + ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Data format is incorrect: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}