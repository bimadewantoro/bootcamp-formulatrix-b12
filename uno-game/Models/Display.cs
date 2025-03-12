using UnoGame.Interfaces;
using UnoGame.Enums;

namespace UnoGame.Models
{
    public class Display : IDisplay
    {
        private const ConsoleColor DefaultBgColor = ConsoleColor.Black;

        private readonly Dictionary<Color, ConsoleColor> _colorMapping = new Dictionary<Color, ConsoleColor>
        {
            { Color.Red, ConsoleColor.Red },
            { Color.Green, ConsoleColor.Green },
            { Color.Blue, ConsoleColor.Blue },
            { Color.Yellow, ConsoleColor.Yellow },
            { Color.Wild, ConsoleColor.White }
        };

        public string ReceiveInput()
        {
            return Console.ReadLine();
        }

        public void ClearScreen()
        {
            Console.Clear();
        }

        public void DisplayTitle()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
  _    _ _   _  ____     _____          __  __ ______ 
 | |  | | \ | |/ __ \   / ____|   /\   |  \/  |  ____|
 | |  | |  \| | |  | | | |  __   /  \  | \  / | |__   
 | |  | | . ` | |  | | | | |_ | / /\ \ | |\/| |  __|  
 | |__| | |\  | |__| | | |__| |/ ____ \| |  | | |____ 
  \____/|_| \_|\____/   \_____/_/    \_\_|  |_|______|
                                                     
");
            Console.ResetColor();
        }

        public void DisplayWelcomeMessage()
        {
            DisplayTitle();
            Console.WriteLine("\nWelcome to the UNO Card Game!");
            Console.WriteLine("---------------------------");
            Console.WriteLine("\nPress any key to start...");
            Console.ReadKey(true);
            ClearScreen();
        }

        public void DisplayGameState(GameController game, IPlayer currentPlayer, bool showCards = true)
        {
            ClearScreen();
            DisplayTitle();


            Console.WriteLine($"\nRound: {game.RoundCount}");

            string direction = game.GetTurnDirection() > 0 ? "Clockwise" : "Counter-Clockwise";
            Console.WriteLine($"Direction: {direction}");

            Console.Write("\nTop Card: ");
            DisplayCard(game.LastPlayedCard);
            Console.WriteLine();

            Console.WriteLine("\nPlayers:");
            foreach (var player in game.GetPlayers())
            {
                if (player.Equals(currentPlayer))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("→ "); // Current player indicator
                }
                else
                {
                    Console.Write("  ");
                }

                Console.WriteLine($"{player.Name} - {game.CountCardInHand(player)} cards (Score: {game.GetPlayerScore(player)})");
                Console.ResetColor();
            }

            if (showCards && currentPlayer != null)
            {
                DisplayPlayerHand(game, currentPlayer);
            }
        }

        public void DisplayPlayerHand(GameController game, IPlayer player)
        {
            var cards = game.GetPlayerCards(player);
            if (cards == null || cards.Count == 0)
            {
                Console.WriteLine("\nYou have no cards!");
                return;
            }

            Console.WriteLine($"\n{player.Name}'s Hand:");
            Console.WriteLine("-----------------");

            for (int i = 0; i < cards.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                DisplayCard(cards[i]);

                if (game.IsCardPlayable(cards[i]))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" (Playable)");
                    Console.ResetColor();
                }

                Console.WriteLine();
            }
        }

        public void DisplayCard(ICard card)
        {
            if (card == null)
            {
                Console.Write("[Empty]");
                return;
            }

            ConsoleColor bgColor = _colorMapping.ContainsKey(card.Color) ?
                _colorMapping[card.Color] : DefaultBgColor;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = bgColor;

            string cardText;
            if (card.Effect == Effect.NoEffect)
            {
                cardText = $" {(int)card.Score} ";
            }
            else if (card.Effect == Effect.Wild || card.Effect == Effect.WildDrawFour)
            {
                cardText = $" {card.Effect} ";
            }
            else
            {
                cardText = $" {card.Effect} ";
            }

            Console.Write(cardText);
            Console.ResetColor();
        }

        public void DisplayRoundWinner(IPlayer winner, int roundScore)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{winner.Name} wins the round!");
            Console.WriteLine($"Scored {roundScore} points!");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        public void DisplayGameWinner(IPlayer winner, int finalScore)
        {
            ClearScreen();
            DisplayTitle();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n🎉 GAME OVER! 🎉");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{winner.Name.ToUpper()} WINS THE GAME!");
            Console.WriteLine($"Final Score: {finalScore} points");
            Console.ResetColor();
            Console.WriteLine("\nThank you for playing UNO!");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey(true);
        }

        public void DisplayMessage(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public int GetNumericInput(int min, int max, string prompt = "Enter your choice: ")
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
                Console.ResetColor();
            }
        }

        public bool GetYesNoInput(string prompt = "Confirm? (y/n): ")
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "y" || input == "yes")
                {
                    return true;
                }

                if (input == "n" || input == "no")
                {
                    return false;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                Console.ResetColor();
            }
        }

        public void DisplayGame()
        {
            Console.WriteLine("Game is in progress.");
        }

        public bool AskForWildDrawFourChallenge(IPlayer previousPlayer)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("═════════════════════════════════════════════════");
            Console.WriteLine("              WILD DRAW FOUR CHALLENGE           ");
            Console.WriteLine("═════════════════════════════════════════════════");
            Console.ResetColor();

            Console.WriteLine($"\n{previousPlayer.Name} just played a Wild Draw Four card.");
            Console.WriteLine("\nUNO RULE: This card can only be played if the player has NO matching color cards.");
            Console.WriteLine("\nIf you challenge:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("- If successful: They must draw 4 cards instead of you.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("- If failed: You must draw 6 cards instead of 4.");
            Console.ResetColor();

            return GetYesNoInput("\nWould you like to challenge this play? (y/n): ");
        }
    }
}