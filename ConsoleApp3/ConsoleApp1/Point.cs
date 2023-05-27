// See https://aka.ms/new-console-template for more information
public struct Point
{
    int X;
    int Y;
    int Z;

    public Point(int x, int y, int z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public static Point operator+(Point first, Point second)
    {
        return new Point(first.X + second.X, first.Y + second.Y, first.Z + second.Z);
    }

    public static Point operator -(Point first, Point second)
    {
        return new Point(first.X - second.X, first.Y - second.Y, first.Z - second.Z);
    }

    public static Point operator *(Point first, Point second)
    {
        return new Point(first.X * second.X, first.Y * second.Y, first.Z * second.Z);
    }

    public static Point Multiply(Point first, Point second)
    {
        return new Point(first.X * second.X, first.Y * second.Y, first.Z * second.Z);
    }
}

