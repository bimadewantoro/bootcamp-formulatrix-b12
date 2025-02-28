namespace Advanced.Delegates
{
    public class Delegates
    {
        public delegate void DisplayMessage(string message);

        public static void Run()
        {
            DisplayMessage displayMessage = new DisplayMessage(Display);
            displayMessage += DisplayUpperCase;
            displayMessage += DisplayLowerCase;

            displayMessage("HellO, WorlD!");
        }

        public static void Display(string message)
        {
            Console.WriteLine("Original Message: " + message);
        }

        public static void DisplayUpperCase(string message)
        {
            Console.WriteLine("Upper Case: " + message.ToUpper());
        }

        public static void DisplayLowerCase(string message)
        {
            Console.WriteLine("Lower Case: " + message.ToLower());
        }
    }
}