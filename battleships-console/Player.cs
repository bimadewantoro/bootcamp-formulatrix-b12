public class Player
{
    private string name;
    private Board ownBoard;
    private Board trackingBoard;

    // Delegate declarations
    public delegate void ShipPlacedHandler(Ship ship, Coordinate position, Orientation orientation);
    public delegate void ShotFiredHandler(Coordinate position, ShotResult result);
    private ShipPlacedHandler? shipPlacedCallback;
    private ShotFiredHandler? shotFiredCallback;

    public Player(string name)
    {
        this.name = name;
        ownBoard = new Board();
        trackingBoard = new Board();
    }

    public string Name => name;
    public Board OwnBoard => ownBoard;
    public Board TrackingBoard => trackingBoard;

    public bool PlaceShip(Ship ship, Coordinate position, Orientation orientation)
    {
        bool success = ownBoard.PlaceShip(ship, position, orientation);
        if (success && shipPlacedCallback != null)
        {
            shipPlacedCallback(ship, position, orientation);
        }
        return success;
    }

    public ShotResult FireShot(Coordinate coordinate, Player opponent)
    {
        ShotResult result = opponent.ReceiveShot(coordinate);

        // Update tracking board based on result
        Cell cell = trackingBoard.GetCell(coordinate);

        // Set the status directly based on result
        if (result == ShotResult.HIT || result == ShotResult.SUNK)
        {
            // Use a new method to directly set cell status
            cell.SetStatusDirectly(CellStatus.HIT);
        }
        else if (result == ShotResult.MISS)
        {
            cell.SetStatusDirectly(CellStatus.MISS);
        }

        if (shotFiredCallback != null)
        {
            shotFiredCallback(coordinate, result);
        }

        return result;
    }

    public ShotResult ReceiveShot(Coordinate coordinate)
    {
        return ownBoard.ReceiveShot(coordinate);
    }

    public bool AllShipsSunk()
    {
        return ownBoard.AllShipsSunk();
    }

    public void SetShipPlacedCallback(ShipPlacedHandler callback)
    {
        shipPlacedCallback = callback;
    }

    public void SetShotFiredCallback(ShotFiredHandler callback)
    {
        shotFiredCallback = callback;
    }
}