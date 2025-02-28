namespace LearnCSharp
{
    public class TypeBasics
    {
        public static void Run()
        {
            // Declare a variable of type int with a value of 42
            int myInt = 42;
            Console.WriteLine(myInt);

            // Declare a variable of type double with a value of 42.0
            double myDouble = 42.0;
            Console.WriteLine(myDouble);

            // Declare a variable of type string with a value of "42"
            string myString = "42";
            Console.WriteLine(myString);

            // Declare a variable of type bool with a value of true
            bool myBool = true;
            Console.WriteLine(myBool);

            // Declare a variable of type char with a value of '4'
            char myChar = '4';
            Console.WriteLine(myChar);

            // Declare a variable of type int with a value of 42
            // and then assign a value of 43
            int myInt2 = 42;
            myInt2 = 43;
            Console.WriteLine(myInt2);

            // Declare a variable of type double with a value of 42.0
            // and then assign a value of 43.0
            double myDouble2 = 42.0;
            myDouble2 = 43.0;
            Console.WriteLine(myDouble2);

            // Declare a variable of type string with a value of "42"
            // and then assign a value of "43"
            string myString2 = "42";
            myString2 = "43";
            Console.WriteLine(myString2);

            // Declare a variable of type bool with a value of true
            // and then assign a value of false
            bool myBool2 = true;
            myBool2 = false;
            Console.WriteLine(myBool2);

            // Declare a variable of type char with a value of '4'
            // and then assign a value of '3'
            char myChar2 = '4';
            myChar2 = '3';
            Console.WriteLine(myChar2);

            // Declare a variable of type int with a value of 42
            // and then assign a value of    42.0
            int myInt3 = 42;
            // myInt3 = 42.0; // This will not compile
            Console.WriteLine(myInt3);

            // Declare a variable of type double with a value of 42.0
            // and then assign a value of 42
            double myDouble3 = 42.0;
            myDouble3 = 42; // This will compile
            Console.WriteLine(myDouble3);
        }
    }
}