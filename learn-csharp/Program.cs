using Advanced.ArrayOfDelegates;
using Advanced.Delegates;
using Advanced.EventManager;
using Advanced.MultipleEvenHandler;
using Advanced.ObserverDesignPattern;
using Advanced.SimpleException;
using Advanced.MultipleException;
using Advanced.FilteringException;
using Advanced.FireAlarm;
using Advanced.EmployeePromotion;
using Advanced.SpeedWarning;
using CreatingTypes.Classes;
using CreatingTypes.Inheritance;
using CreatingTypes.Interfaces;
using FrameworkFundamental.StringText;
using FrameworkFundamental.DateTimes;
using FrameworkFundamental.FormatingParsing;
using FrameworkFundamental.ConversionMechanisms;
using FrameworkFundamental.Numbers;
using FrameworkFundamental.Enums;
using FrameworkFundamental.EqualityComparison;
using Collections.Enumeration;

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
                // Basic topics
                case "syntax":
                    Syntax.Run();
                    break;
                case "types":
                    TypeBasics.Run();
                    break;
                case "numeric":
                    NumericTypes.Run();
                    break;
                // Creating types
                case "classes":
                    Classes.Run();
                    break;
                case "inheritance":
                    Inheritance.Run();
                    break;
                case "interfaces":
                    Interface.Run();
                    break;
                // Advanced topics
                case "delegates":
                    Delegates.Run();
                    break;
                case "arrays-of-delegates":
                    ArrayOfDelegates.Run();
                    break;
                case "fire-alarm":
                    FireAlarm.Run();
                    break;
                case "employee-promotion":
                    EmployeePromotion.Run();
                    break;
                case "events":
                    EventManager.Run();
                    break;
                case "multiple-events":
                    MultipleEventHandler.Run();
                    break;
                case "observer":
                    ObserverDesignPattern.Run();
                    break;
                case "speed-warning":
                    SpeedWarning.Run();
                    break;
                case "exception":
                    SimpleException.Run();
                    break;
                case "multiple-exception":
                    MultipleException.Run();
                    break;
                case "filtering-exception":
                    FilteringException.Run();
                    break;
                // Framework fundamentals
                case "string":
                    StringText.Run();
                    break;
                case "datetime":
                    DateTimes.Run();
                    break;
                case "formatting":
                    FormatingParsing.Run();
                    break;
                case "conversion":
                    ConversionMechanisms.Run();
                    break;
                case "numbers":
                    Numbers.Run();
                    break;
                case "enums":
                    Enums.Run();
                    break;
                case "equality":
                    EqualityComparison.Run();
                    break;
                case "enumeration":
                    Enumeration.Run();
                    break;
                default:
                    Console.WriteLine("Unknown topic.");
                    break;
            }
        }
    }
}