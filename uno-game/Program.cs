using UnoGame.Interfaces;
using UnoGame.Models;

namespace UnoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Display display = new Display();
            display.DisplayWelcomeMessage();

            List<IPlayer> players = CreatePlayers(display);

            IDeck deck = new Deck();

            GameController game = new GameController(players, deck, display);

            game.OnRoundStart += () =>
            {
                display.DisplayMessage("\nNew round started!", ConsoleColor.Cyan);
                Thread.Sleep(1500);
            };

            game.OnCardPlay += (card) =>
            {
                display.DisplayMessage($"\nCard played: ", ConsoleColor.White);
                display.DisplayCard(card);
                Console.WriteLine();
                Thread.Sleep(1000);
            };

            PlayGame(game, display);

            IPlayer winner = game.GetGameWinner();
            display.DisplayGameWinner(winner, game.GetPlayerScore(winner));
        }

        static List<IPlayer> CreatePlayers(Display display)
        {
            List<IPlayer> players = new List<IPlayer>();

            display.DisplayMessage("How many players? (2-10): ", ConsoleColor.Cyan);
            int playerCount = display.GetNumericInput(2, 10);

            for (int i = 0; i < playerCount; i++)
            {
                display.DisplayMessage($"\nEnter name for Player {i + 1}: ", ConsoleColor.Cyan);
                string name = display.ReceiveInput();

                if (string.IsNullOrWhiteSpace(name))
                {
                    name = $"Player {i + 1}";
                }

                players.Add(new Player(i + 1, name));
            }

            return players;
        }

        static void PlayGame(GameController game, Display display)
        {
            while (!game.IsGameOver())
            {
                game.StartNewRound();

                PlayRound(game, display);

                IPlayer roundWinner = game.GetRoundWinner();
                int roundScore = game.CountRoundScore();

                display.DisplayRoundWinner(roundWinner, roundScore);

                if (game.IsGameOver())
                {
                    break;
                }

                game.ResetRoundWinner();
            }
        }

        static void PlayRound(GameController game, Display display)
        {
            while (true)
            {
                IPlayer currentPlayer = game.GetCurrentPlayer();

                display.DisplayGameState(game, currentPlayer);

                ProcessPlayerTurn(game, display, currentPlayer);

                if (game.IsRoundOver())
                {
                    break;
                }

                if (!game.NextTurn())
                {
                    break;
                }

                Thread.Sleep(1000);
            }
        }

        static void ProcessPlayerTurn(GameController game, Display display, IPlayer player)
        {
            display.DisplayMessage($"\n{player.Name}'s turn", ConsoleColor.Yellow);

            List<ICard> playerCards = game.GetPlayerCards(player);

            bool hasPlayableCard = game.HasPlayableCard(player);

            if (hasPlayableCard)
            {
                display.DisplayMessage("\nOptions:", ConsoleColor.Cyan);
                display.DisplayMessage("1. Play a card", ConsoleColor.White);
                display.DisplayMessage("2. Draw a card", ConsoleColor.White);

                int choice = display.GetNumericInput(1, 2);

                if (choice == 1)
                {
                    PlayCard(game, display, player);
                }
                else
                {
                    DrawCardAndMaybePLay(game, display, player);
                }
            }
            else
            {
                display.DisplayMessage("\nYou have no playable cards. Drawing a card...", ConsoleColor.Yellow);
                Thread.Sleep(1000);

                // Draw a card
                DrawCardAndMaybePLay(game, display, player);
            }

            if (game.CountCardInHand(player) == 1)
            {
                display.DisplayMessage("\nUNO!", ConsoleColor.Magenta);
                game.SayUno();
                Thread.Sleep(1000);
            }

            game.EndTurn();
        }

        static void PlayCard(GameController game, Display display, IPlayer player)
        {
            List<ICard> playerCards = game.GetPlayerCards(player);

            List<int> playableCardIndices = new List<int>();
            for (int i = 0; i < playerCards.Count; i++)
            {
                if (game.IsCardPlayable(playerCards[i]))
                {
                    playableCardIndices.Add(i);
                }
            }

            if (playableCardIndices.Count == 0)
            {
                display.DisplayMessage("\nNo playable cards available. You must draw a card.", ConsoleColor.Red);
                DrawCardAndMaybePLay(game, display, player);
                return;
            }

            display.DisplayMessage("\nChoose a card to play (enter card number):", ConsoleColor.Cyan);

            while (true)
            {
                int cardIndex = display.GetNumericInput(1, playerCards.Count) - 1;

                if (game.IsCardPlayable(playerCards[cardIndex]))
                {
                    game.PlayCard(player, playerCards[cardIndex]);
                    break;
                }
                else
                {
                    display.DisplayMessage("\nThat card cannot be played. Choose another or press 'C' to cancel.", ConsoleColor.Red);

                    Console.WriteLine("Press any key to try again or 'C' to cancel");
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.C)
                    {
                        ProcessPlayerTurn(game, display, player);
                        return;
                    }
                }
            }
        }

        static void DrawCardAndMaybePLay(GameController game, Display display, IPlayer player)
        {
            display.DisplayMessage("\nDrawing a card...", ConsoleColor.Yellow);
            Thread.Sleep(1000);

            // Draw card
            ICard drawnCard = null;
            if (game.DrawCard(player))
            {
                List<ICard> playerCards = game.GetPlayerCards(player);
                drawnCard = playerCards.Last();

                display.DisplayMessage("You drew: ", ConsoleColor.White);
                display.DisplayCard(drawnCard);
                Console.WriteLine();
                Thread.Sleep(1000);

                if (game.IsCardPlayable(drawnCard))
                {
                    display.DisplayMessage("\nYou can play this card.", ConsoleColor.Green);
                    display.DisplayMessage("Do you want to play it? (y/n)", ConsoleColor.Cyan);

                    if (display.GetYesNoInput())
                    {
                        game.PlayCard(player, drawnCard);
                    }
                }
                else
                {
                    display.DisplayMessage("\nThis card cannot be played.", ConsoleColor.Red);
                    Thread.Sleep(1000);
                }
            }
            else
            {
                display.DisplayMessage("\nNo cards left to draw! Recycling discard pile...", ConsoleColor.Yellow);
                game.RecycleDiscardedCards();
                Thread.Sleep(1000);

                if (game.DrawCard(player))
                {
                    List<ICard> playerCards = game.GetPlayerCards(player);
                    drawnCard = playerCards.Last();

                    display.DisplayMessage("You drew: ", ConsoleColor.White);
                    display.DisplayCard(drawnCard);
                    Console.WriteLine();
                    Thread.Sleep(1000);

                    if (game.IsCardPlayable(drawnCard))
                    {
                        display.DisplayMessage("\nYou can play this card.", ConsoleColor.Green);
                        display.DisplayMessage("Do you want to play it? (y/n)", ConsoleColor.Cyan);

                        if (display.GetYesNoInput())
                        {
                            game.PlayCard(player, drawnCard);
                        }
                    }
                    else
                    {
                        display.DisplayMessage("\nThis card cannot be played.", ConsoleColor.Red);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    display.DisplayMessage("\nStill no cards to draw! Skipping turn.", ConsoleColor.Red);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}