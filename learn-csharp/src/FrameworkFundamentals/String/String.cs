namespace FrameworkFundamental.StringText
{
    public class StringText
    {
        public static void Run()
        {
            // The char data type is used to store a single character.
            char myChar = 'A';
            Console.WriteLine(myChar);

            // The string data type is used to store a sequence of characters.
            string myString = "Hello, World!";
            Console.WriteLine(myString);

            // Searching Within Strings
            string text = "The quick brown fox jumps over the lazy dog.";
            Console.WriteLine(text.Contains("fox"));
            Console.WriteLine(text.EndsWith("fox"));

            // Manipulating Strings
            string name = "John";
            Console.WriteLine(name.Insert(4, " Kerry"));

            string text1 = "The quick brown fox jumps over the lazy dog.";
            Console.WriteLine(text1.PadLeft(50));
            Console.WriteLine(text1.PadRight(50));

            string text2 = "The quick brown fox jumps over the lazy dog.";
            Console.WriteLine(text2.Remove(10));

            // Comparing Strings
            string str1 = "Hello";
            string str2 = "Hello";
            Console.WriteLine(str1 == str2);

            // Concatenating Strings
            string firstName = "John";
            string lastName = "Doe";
            string name1 = firstName + " " + lastName;
            Console.WriteLine(name1);
        }
    }
}