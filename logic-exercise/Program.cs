using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var printer = new DivisibilityPrinter();

        printer.AddRule(3, "foo");
        printer.AddRule(4, "baz");
        printer.AddRule(5, "bar");
        printer.AddRule(7, "jazz");
        printer.AddRule(9, "huzz");

        printer.PrintSequence(105);
    }
}

public class DivisibilityPrinter
{
    private readonly Dictionary<int, string> _rules = new Dictionary<int, string>();

    public bool AddRule(int divisor, string output)
    {
        if (divisor <= 0)
        {
            Console.WriteLine($"Error: Divisor must be greater than zero, received: {divisor}");
            return false;
        }

        if (string.IsNullOrEmpty(output))
        {
            Console.WriteLine("Error: Output string cannot be null or empty");
            return false;
        }

        _rules[divisor] = output;
        return true;
    }

    public string GenerateOutput(int number)
    {
        if (number <= 0)
        {
            Console.WriteLine($"Warning: Number must be greater than zero, received: {number}");
            return number.ToString();
        }

        var result = new StringBuilder();
        bool hasPrinted = false;

        foreach (var rule in _rules.OrderBy(r => r.Key))
        {
            if (number % rule.Key == 0)
            {
                result.Append(rule.Value);
                hasPrinted = true;
            }
        }

        if (!hasPrinted)
            result.Append(number);

        return result.ToString();
    }

    public string GenerateSequence(int n, string separator = ", ")
    {
        if (n <= 0)
        {
            Console.WriteLine($"Error: Upper limit must be greater than zero, received: {n}");
            return string.Empty;
        }

        var sequence = new StringBuilder();

        for (int i = 1; i <= n; i++)
        {
            sequence.Append(GenerateOutput(i));

            if (i < n)
                sequence.Append(separator);
        }

        return sequence.ToString();
    }

    public void PrintSequence(int n)
    {
        if (n <= 0)
        {
            Console.WriteLine($"Error: Upper limit must be greater than zero, received: {n}");
            return;
        }

        for (int i = 1; i <= n; i++)
        {
            Console.Write(GenerateOutput(i));

            if (i < n)
                Console.Write(", ");
        }
        Console.WriteLine();
    }
}