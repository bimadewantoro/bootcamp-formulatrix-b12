using System;
using System.Collections.Generic;
using UnoGame.Interfaces;
using UnoGame.Models;

namespace UnoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to UNO Game!");
            
            IDisplay display = new Display();
            
            IDeck deck = new Deck();
            
            List<IPlayer> players = new List<IPlayer>
            {
                new Player(1, "Alice"),
                new Player(2, "Bob"),
                new Player(3, "Charlie"),
                new Player(4, "Diana")
            };
            
            GameController game = new GameController(players, deck, display);
            
            game.OnRoundStart += () => 
            {
                Console.WriteLine("New round started!");
            };
            
            game.OnCardPlay += (card) => 
            {
                Console.WriteLine($"Card played: {card}");
            };
            
            game.DistributeCards();
            
            Console.WriteLine("Game initialized. This is a minimal demonstration of the UNO game implementation.");
            Console.WriteLine("For a full game, you would need to implement a game loop that handles player turns and input.");
            
            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name}'s hand ({game.CountCardInHand(player)} cards):");
            }
            
            Console.WriteLine($"Top card: {game.LastPlayedCard}");
            
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}