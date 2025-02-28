using System;

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
                default:
                    Console.WriteLine("Unknown topic.");
                    break;
            }
        }
    }
}