public class Cell
{
    private CellStatus status;
    private Ship? ship;
    private Coordinate? position;

    // Delegate declaration
    public delegate void CellStatusChangedHandler(CellStatus oldStatus, CellStatus newStatus);
    private CellStatusChangedHandler? cellStatusChangedCallback;

    public Cell()
    {
        status = CellStatus.EMPTY;
        ship = null;
    }

    public void SetPosition(Coordinate position)
    {
        this.position = position;
    }

    public ShotResult Hit()
    {
        if (status == CellStatus.HIT || status == CellStatus.MISS)
            return ShotResult.ALREADY_SHOT;

        CellStatus oldStatus = status;
        if (ship != null)
        {
            status = CellStatus.HIT;
            if (position == null)
                throw new InvalidOperationException("Cell position cannot be null when hitting a ship.");
            bool isSunk = ship.Hit(position);

            if (cellStatusChangedCallback != null)
            {
                cellStatusChangedCallback(oldStatus, status);
            }

            return isSunk ? ShotResult.SUNK : ShotResult.HIT;
        }
        else
        {
            status = CellStatus.MISS;

            if (cellStatusChangedCallback != null)
            {
                cellStatusChangedCallback(oldStatus, status);
            }

            return ShotResult.MISS;
        }
    }

    public bool PlaceShip(Ship ship)
    {
        if (status != CellStatus.EMPTY)
            return false;

        this.ship = ship;
        status = CellStatus.SHIP;

        if (cellStatusChangedCallback != null)
        {
            cellStatusChangedCallback(CellStatus.EMPTY, status);
        }

        return true;
    }

    public bool HasShip()
    {
        return ship != null;
    }

    public bool WasShot()
    {
        return status == CellStatus.HIT || status == CellStatus.MISS;
    }

    public CellStatus Status => status;

    public void SetCellStatusChangedCallback(CellStatusChangedHandler callback)
    {
        cellStatusChangedCallback = callback;
    }

    public void SetStatusDirectly(CellStatus newStatus)
    {
        CellStatus oldStatus = status;
        status = newStatus;

        if (cellStatusChangedCallback != null)
        {
            cellStatusChangedCallback(oldStatus, status);
        }
    }
}