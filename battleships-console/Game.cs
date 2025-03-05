public class Game
{
    private Player[] players;
    private int currentPlayerIndex;
    private GameState state;
    private int setupCompletedCount;

    // Delegate declarations
    public delegate void TurnChangedHandler(Player currentPlayer);
    public delegate void GameOverHandler(Player winner);
    public delegate void SetupCompletedHandler(Player player);
    private TurnChangedHandler? turnChangedCallback;
    private GameOverHandler? gameOverCallback;
    private SetupCompletedHandler? setupCompletedCallback;

    public Game()
    {
        players = new Player[2];
        currentPlayerIndex = 0;
        state = GameState.SETUP;
        setupCompletedCount = 0;
    }

    public void Initialize(string player1Name, string player2Name)
    {
        players[0] = new Player(player1Name);
        players[1] = new Player(player2Name);
        state = GameState.SETUP;
        setupCompletedCount = 0;
    }

    public void CompleteSetup(Player player)
    {
        // Verify the player is part of the game
        int playerIndex = Array.IndexOf(players, player);

        if (playerIndex != -1)
        {
            setupCompletedCount++;

            // Notify via delegate
            if (setupCompletedCallback != null)
            {
                setupCompletedCallback(player);
            }

            // If all players have completed setup, change state to PLAYING
            if (setupCompletedCount >= players.Length)
            {
                state = GameState.PLAYING;
            }
        }
    }

    public void SwitchTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % 2;

        // Invoke the delegate if it has been assigned
        if (turnChangedCallback != null)
        {
            turnChangedCallback(CurrentPlayer);
        }
    }

    public ShotResult ProcessShot(Coordinate coordinate)
    {
        // Only process shots if the game is in the PLAYING state
        if (state != GameState.PLAYING)
            return ShotResult.INVALID;

        Player opponent = players[(currentPlayerIndex + 1) % 2];

        ShotResult result = CurrentPlayer.FireShot(coordinate, opponent);

        // Check if the game is over after this shot
        if (opponent.AllShipsSunk())
        {
            state = GameState.FINISHED;
            if (gameOverCallback != null)
            {
                gameOverCallback(CurrentPlayer);
            }
        }

        return result;
    }

    public bool IsGameOver()
    {
        return state == GameState.FINISHED;
    }

    public Player GetWinner()
    {
        if (state != GameState.FINISHED)
            return null!;

        return players[0].AllShipsSunk() ? players[1] : players[0];
    }

    public Player CurrentPlayer => players[currentPlayerIndex];
    public GameState State => state;

    // Methods to set delegate callbacks
    public void SetTurnChangedCallback(TurnChangedHandler callback)
    {
        turnChangedCallback = callback;
    }

    public void SetGameOverCallback(GameOverHandler callback)
    {
        gameOverCallback = callback;
    }

    public void SetSetupCompletedCallback(SetupCompletedHandler callback)
    {
        setupCompletedCallback = callback;
    }
}