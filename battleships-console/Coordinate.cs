public class Coordinate
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Coordinate other)
            return false;

        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }

    public override string ToString()
    {
        return $"({X},{Y})";
    }
}