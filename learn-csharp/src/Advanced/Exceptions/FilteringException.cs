namespace Advanced.FilteringException
{
    public class FilteringException
    {
        public static void Run()
        {
            try
            {
                var result = ConvertToNumber("123abc");
                Console.WriteLine($"Processing result: {result}");
            }
            catch (FormatException ex) when (ex.Message.Contains("invalid format"))
            {
                Console.WriteLine("Data has an invalid format. Please check your inputs.");
            }
            catch (FormatException ex) when (ex.Message.Contains("empty string"))
            {
                Console.WriteLine("No data provided. Please enter some numeric data.");
            }
            catch (OverflowException ex) when (ex.Message.Contains("too large"))
            {
                Console.WriteLine("Data is too large. Please enter a smaller number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        private static int ConvertToNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new FormatException("empty string");
            }

            if (!int.TryParse(input, out var number))
            {
                throw new FormatException("invalid format");
            }

            if (number > int.MaxValue)
            {
                throw new OverflowException("too large");
            }

            return number;
        }
    }
}