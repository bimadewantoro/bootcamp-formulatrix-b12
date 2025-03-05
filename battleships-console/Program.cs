class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Battleships!");

        // Initialize game
        Game game = new Game();
        game.Initialize("Player 1", "Player 2");

        // Set up callbacks
        game.SetTurnChangedCallback(player =>
            Console.WriteLine($"Turn changed to {player.Name}"));

        game.SetGameOverCallback(winner =>
            Console.WriteLine($"Game over! {winner.Name} wins!"));

        game.SetSetupCompletedCallback(player =>
            Console.WriteLine($"{player.Name} has completed setup"));

        // Place ships for both players (simplified for demo)
        PlaceShipsForDemo(game.CurrentPlayer);
        game.CompleteSetup(game.CurrentPlayer);

        game.SwitchTurn();

        PlaceShipsForDemo(game.CurrentPlayer);
        game.CompleteSetup(game.CurrentPlayer);

        // Switch back to player 1 for first turn
        game.SwitchTurn();

        // Simple game loop
        while (!game.IsGameOver())
        {
            Console.WriteLine($"\n{game.CurrentPlayer.Name}'s turn:");
            DisplayBoard(game.CurrentPlayer.TrackingBoard, true);

            Console.WriteLine("Enter coordinates to fire at (e.g. A5):");
            string input = Console.ReadLine() ?? "";

            if (TryParseCoordinate(input, out Coordinate coordinate))
            {
                ShotResult result = game.ProcessShot(coordinate);
                Console.WriteLine($"Result: {result}");

                if (result != ShotResult.INVALID && result != ShotResult.ALREADY_SHOT)
                {
                    game.SwitchTurn();
                }
            }
            else
            {
                Console.WriteLine("Invalid coordinate format! Try again.");
            }
        }

        Console.WriteLine("\nGame Over!");
        Console.WriteLine($"Winner: {game.GetWinner().Name}");
    }

    private static void PlaceShipsForDemo(Player player)
    {
        Console.WriteLine($"\n{player.Name} placing ships...");

        player.PlaceShip(new Ship(ShipType.CARRIER),
            new Coordinate(0, 0), Orientation.HORIZONTAL);

        player.PlaceShip(new Ship(ShipType.BATTLESHIP),
            new Coordinate(0, 2), Orientation.HORIZONTAL);

        player.PlaceShip(new Ship(ShipType.CRUISER),
            new Coordinate(0, 4), Orientation.HORIZONTAL);

        player.PlaceShip(new Ship(ShipType.SUBMARINE),
            new Coordinate(0, 6), Orientation.HORIZONTAL);

        player.PlaceShip(new Ship(ShipType.DESTROYER),
            new Coordinate(0, 8), Orientation.HORIZONTAL);

        DisplayBoard(player.OwnBoard, false);
    }

    // Helper method to parse coordinate input
    private static bool TryParseCoordinate(string input, out Coordinate coordinate)
    {
        coordinate = new Coordinate(0, 0);

        if (input.Length < 2)
            return false;

        input = input.ToUpper();
        char col = input[0];

        if (col < 'A' || col > 'J')
            return false;

        if (!int.TryParse(input.Substring(1), out int row))
            return false;

        if (row < 1 || row > 10)
            return false;

        coordinate = new Coordinate(col - 'A', row - 1);
        return true;
    }

    // Helper method to display the board
    private static void DisplayBoard(Board board, bool isTrackingBoard)
    {
        Console.WriteLine("\n   A B C D E F G H I J");
        Console.WriteLine("  --------------------");

        for (int y = 0; y < board.Size; y++)
        {
            Console.Write($"{y + 1}{(y < 9 ? " " : "")}|");

            for (int x = 0; x < board.Size; x++)
            {
                Cell cell = board.GetCell(x, y);
                char symbol = GetCellSymbol(cell, isTrackingBoard);
                Console.Write($"{symbol}|");
            }

            Console.WriteLine();
            Console.WriteLine("  --------------------");
        }
    }

    // Helper method to get cell display symbol
    private static char GetCellSymbol(Cell cell, bool isTrackingBoard)
    {
        switch (cell.Status)
        {
            case CellStatus.HIT:
                return 'X';
            case CellStatus.MISS:
                return 'O';
            case CellStatus.SHIP:
                return isTrackingBoard ? ' ' : 'S';
            default:
                return ' ';
        }
    }
}