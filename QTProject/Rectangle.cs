/// <summary>
/// Represents a rectangle in a 2D space.
/// </summary>
public class Rectangle
{
    /// <summary>
    /// Gets or sets the X coordinate of the rectangle.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate of the rectangle.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Gets or sets the length of the rectangle.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Gets or sets the width of the rectangle.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle"/> class.
    /// </summary>
    /// <param name="x">The X coordinate of the rectangle.</param>
    /// <param name="y">The Y coordinate of the rectangle.</param>
    /// <param name="length">The length of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    public Rectangle(int x, int y, int length, int width)
    {
        X = x;
        Y = y;
        Length = length;
        Width = width;
    }

    /// <summary>
    /// Determines whether the current rectangle contains another rectangle.
    /// </summary>
    /// <param name="other">The rectangle to check.</param>
    /// <returns>True if the current rectangle contains the other rectangle; otherwise, false.</returns>
    public bool Contains(Rectangle other)
    {
        return X <= other.X &&
               Y <= other.Y &&
               X + Length >= other.X + other.Length &&
               Y + Width >= other.Y + other.Width;
    }

    /// <summary>
    /// Determines if the current rectangle intersects with another rectangle.
    /// </summary>
    /// <param name="other">The rectangle to check for intersection.</param>
    /// <returns>True if the rectangles intersect; otherwise, false.</returns>
    public bool Intersects(Rectangle other)
    {
        return !(other.X > X + Length || 
                 other.X + other.Length < X || 
                 other.Y > Y + Width || 
                 other.Y + other.Width < Y);
    }

    /// <summary>
    /// Returns a string representation of the rectangle.
    /// </summary>
    /// <returns>A string that represents the rectangle.</returns>
    public override string ToString()
    {
        return $"Rectangle at ({X}, {Y}) with dimensions {Length}x{Width}";
    }
}