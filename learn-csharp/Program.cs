using Advanced.ArrayOfDelegates;
using Advanced.Delegates;
using CreatingTypes.Classes;

namespace LearnCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a topic to run.");
                return;
            }

            switch (args[0].ToLower())
            {
                case "hello":
                    HelloWorld.Run();
                    break;
                case "syntax":
                    Syntax.Run();
                    break;
                case "types":
                    TypeBasics.Run();
                    break;
                case "numeric":
                    NumericTypes.Run();
                    break;
                case "classes":
                    Classes.Run();
                    break;
                case "delegates":
                    Delegates.Run();
                    break;
                case "arrays-of-delegates":
                    ArrayOfDelegates.Run();
                    break;
                default:
                    Console.WriteLine("Unknown topic.");
                    break;
            }
        }
    }
}