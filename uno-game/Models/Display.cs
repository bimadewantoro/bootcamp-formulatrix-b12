using System;
using UnoGame.Interfaces;

namespace UnoGame.Models
{
    public class Display : IDisplay
    {
        public string ReceiveInput()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        public void DisplayGame()
        {
            Console.WriteLine("Game display placeholder");
        }

        public void DisplayGame(string message)
        {
            Console.WriteLine(message);
        }
    }
}