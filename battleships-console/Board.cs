public class Board
{
    private int size;
    private Cell[,] grid;
    private List<Ship> ships;

    // Delegate declarations
    public delegate void ShipSunkHandler(Ship sunkShip);
    public delegate void ShotResultHandler(Coordinate position, ShotResult result);
    private ShipSunkHandler? shipSunkCallback;
    private ShotResultHandler? shotResultCallback;

    public Board(int size = 10)
    {
        this.size = size;
        grid = new Cell[size, size];
        ships = new List<Ship>();

        // Initialize grid with empty cells
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                grid[x, y] = new Cell();
            }
        }
    }

    public bool PlaceShip(Ship ship, Coordinate startPosition, Orientation orientation)
    {
        if (!IsValidPlacement(ship, startPosition, orientation))
            return false;

        List<Coordinate> positions = new List<Coordinate>();
        for (int i = 0; i < ship.GetSize(); i++)
        {
            Coordinate pos = orientation == Orientation.HORIZONTAL
                ? new Coordinate(startPosition.X + i, startPosition.Y)
                : new Coordinate(startPosition.X, startPosition.Y + i);

            positions.Add(pos);
            grid[pos.X, pos.Y].SetPosition(pos);
            grid[pos.X, pos.Y].PlaceShip(ship);
        }

        ship.SetPosition(positions, orientation);
        ships.Add(ship);

        // Set up callback for ship hits
        ship.SetShipHitCallback((pos, isSunk) =>
        {
            if (isSunk && shipSunkCallback != null)
            {
                shipSunkCallback(ship);
            }
        });

        return true;
    }

    public ShotResult ReceiveShot(Coordinate position)
    {
        if (!IsValidCoordinate(position))
            return ShotResult.INVALID;

        ShotResult result = grid[position.X, position.Y].Hit();

        if (shotResultCallback != null)
        {
            shotResultCallback(position, result);
        }

        return result;
    }

    public bool IsValidPlacement(Ship ship, Coordinate startPosition, Orientation orientation)
    {
        int shipSize = ship.GetSize();

        // Check if the ship fits on the board
        if (orientation == Orientation.HORIZONTAL)
        {
            if (startPosition.X + shipSize > size)
                return false;
        }
        else
        {
            if (startPosition.Y + shipSize > size)
                return false;
        }

        // Check if the placement overlaps with other ships
        for (int i = 0; i < shipSize; i++)
        {
            Coordinate pos = orientation == Orientation.HORIZONTAL
                ? new Coordinate(startPosition.X + i, startPosition.Y)
                : new Coordinate(startPosition.X, startPosition.Y + i);

            if (!IsValidCoordinate(pos) || grid[pos.X, pos.Y].HasShip())
                return false;
        }

        return true;
    }

    public bool AllShipsSunk()
    {
        foreach (Ship ship in ships)
        {
            if (!ship.IsSunk())
                return false;
        }
        return ships.Count > 0;
    }

    private bool IsValidCoordinate(Coordinate coordinate)
    {
        return coordinate.X >= 0 && coordinate.X < size &&
               coordinate.Y >= 0 && coordinate.Y < size;
    }

    public int Size => size;
    public Cell GetCell(int x, int y) => grid[x, y];
    public Cell GetCell(Coordinate coord) => grid[coord.X, coord.Y];

    public void SetShipSunkCallback(ShipSunkHandler callback)
    {
        shipSunkCallback = callback;
    }

    public void SetShotResultCallback(ShotResultHandler callback)
    {
        shotResultCallback = callback;
    }
}