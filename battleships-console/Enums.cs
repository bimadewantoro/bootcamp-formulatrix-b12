public enum GameState
{
    SETUP,
    PLAYING,
    FINISHED
}

public enum ShipType
{
    CARRIER = 5,
    BATTLESHIP = 4,
    CRUISER = 3,
    SUBMARINE = 3,
    DESTROYER = 2
}

public enum CellStatus
{
    EMPTY,
    SHIP,
    HIT,
    MISS
}

public enum Orientation
{
    HORIZONTAL,
    VERTICAL
}

public enum ShotResult
{
    HIT,
    MISS,
    SUNK,
    INVALID,
    ALREADY_SHOT
}