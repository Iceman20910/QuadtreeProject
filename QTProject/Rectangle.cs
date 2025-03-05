/// <summary>
/// Represents a rectangle in a 2D space.
/// </summary>
public class Rectangle
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }

    public Rectangle(int x, int y, int height, int width)
    {
        X = x;
        Y = y;
        Height = height;
        Width = width;
    }

    public bool Contains(Rectangle other)
    {
        return X <= other.X &&
               Y <= other.Y &&
               X + Width >= other.X + other.Width &&
               Y + Height >= other.Y + other.Height;
    }

    public bool Intersects(Rectangle other)
    {
        return !(other.X > X + Width ||
                 other.X + other.Width < X ||
                 other.Y > Y + Height ||
                 other.Y + other.Height < Y);
    }

    public override string ToString()
    {
        return $"Rectangle at ({X}, {Y}) with dimensions {Width}x{Height}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Rectangle other)
        {
            return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Width, Height);
    }
}