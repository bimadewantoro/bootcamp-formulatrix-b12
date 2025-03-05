public class Ship
{
    private ShipType type;
    private List<Coordinate> positions;
    private Orientation orientation;
    private HashSet<Coordinate> hitPositions;

    // Delegate declaration
    public delegate void ShipHitHandler(Coordinate position, bool isSunk);
    private ShipHitHandler? shipHitCallback;

    public Ship(ShipType type)
    {
        this.type = type;
        positions = new List<Coordinate>();
        hitPositions = new HashSet<Coordinate>();
        orientation = Orientation.HORIZONTAL; // Default
    }

    public void SetPosition(List<Coordinate> positions, Orientation orientation)
    {
        this.positions = positions;
        this.orientation = orientation;
    }

    public bool Hit(Coordinate coordinate)
    {
        // Find the actual coordinate from positions that matches
        Coordinate? actualCoord = positions.FirstOrDefault(p => p.Equals(coordinate));
        if (actualCoord != null && !hitPositions.Contains(actualCoord))
        {
            hitPositions.Add(actualCoord);

            if (shipHitCallback != null)
            {
                shipHitCallback(actualCoord, IsSunk());
            }

            return IsSunk();
        }
        return false;
    }

    public bool IsSunk()
    {
        return positions.Count > 0 && hitPositions.Count == positions.Count;
    }

    public int GetSize()
    {
        return (int)type;
    }

    public ShipType Type => type;
    public Orientation Orientation => orientation;
    public IReadOnlyList<Coordinate> Positions => positions.AsReadOnly();

    public void SetShipHitCallback(ShipHitHandler callback)
    {
        shipHitCallback = callback;
    }
}