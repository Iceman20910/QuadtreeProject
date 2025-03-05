using System;

/// <summary>
/// Represents an internal node in the quadtree, which can have child nodes.
/// </summary>
public class InternalNode : Node
{
    private Node[] children;

    public InternalNode(Rectangle rectangle) : base(rectangle)
    {
        children = new Node[4];
    }

    public override Node Insert(Rectangle rectangle)
    {
        int index = GetQuadrant(rectangle);
        if (children[index] == null)
        {
            children[index] = new LeafNode(GetChildRectangle(index));
        }
        return children[index].Insert(rectangle);
    }

    public override Rectangle? Find(Rectangle rectangle)
    {
        int index = GetQuadrant(rectangle);
        if (children[index] != null)
        {
            return children[index].Find(rectangle);
        }
        return null;
    }

    public override bool Delete(Rectangle rectangle)
    {
        int index = GetQuadrant(rectangle);
        if (children[index] != null)
        {
            return children[index].Delete(rectangle);
        }
        return false;
    }

    public override bool Update(Rectangle rectangle)
    {
        int index = GetQuadrant(rectangle);
        if (children[index] != null)
        {
            return children[index].Update(rectangle);
        }
        return false;
    }

    public override void Dump(int indent)
    {
        Console.WriteLine($"{new string(' ', indent)}Internal Node - ({Rectangle.X}, {Rectangle.Y}) - {Rectangle.Width}x{Rectangle.Height}");
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] != null)
            {
                children[i].Dump(indent + 2);
            }
        }
    }

    private int GetQuadrant(Rectangle rectangle)
    {
        bool right = rectangle.X > Rectangle.X + Rectangle.Width / 2;
        bool top = rectangle.Y < Rectangle.Y + Rectangle.Height / 2;

        if (right && top)
            return 0; // Top-right
        if (!right && top)
            return 1; // Top-left
        if (!right && !top)
            return 2; // Bottom-left
        return 3; // Bottom-right
    }

    private Rectangle GetChildRectangle(int index)
    {
        int halfWidth = Rectangle.Width / 2;
        int halfHeight = Rectangle.Height / 2;

        switch (index)
        {
            case 0: return new Rectangle(Rectangle.X + halfWidth, Rectangle.Y, halfWidth, halfHeight); // Top-right
            case 1: return new Rectangle(Rectangle.X, Rectangle.Y, halfWidth, halfHeight); // Top-left
            case 2: return new Rectangle(Rectangle.X, Rectangle.Y + halfHeight, halfWidth, halfHeight); // Bottom-left
            case 3: return new Rectangle(Rectangle.X + halfWidth, Rectangle.Y + halfHeight, halfWidth, halfHeight); // Bottom-right
            default: throw new ArgumentOutOfRangeException();
        }
    }
}