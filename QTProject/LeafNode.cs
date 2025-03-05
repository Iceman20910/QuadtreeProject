using System;
using System.Collections.Generic;

/// <summary>
/// Represents a leaf node in the quadtree, storing rectangles directly.
/// </summary>
public class LeafNode : Node
{
    private List<Rectangle> rectangles;
    private const int MAX_RECTANGLES = 5;

    public LeafNode(Rectangle rectangle) : base(rectangle)
    {
        rectangles = new List<Rectangle>();
    }

    public override Node Insert(Rectangle rectangle)
    {
        if (rectangles.Exists(r => r.Equals(rectangle)))
        {
            throw new DoubleInsertException("A rectangle already exists at the specified coordinates.");
        }

        if (rectangles.Count < MAX_RECTANGLES)
        {
            rectangles.Add(rectangle);
            return this;
        }
        else
        {
            return Split(rectangle);
        }
    }

    private Node Split(Rectangle rectangle)
    {
        InternalNode newInternalNode = new InternalNode(Rectangle);

        foreach (var rect in rectangles)
        {
            newInternalNode.Insert(rect);
        }

        newInternalNode.Insert(rectangle);

        return newInternalNode;
    }

    public override Rectangle? Find(Rectangle rectangle)
    {
        foreach (Rectangle rect in rectangles)
        {
            if (rect.Contains(rectangle))
                return rect;
        }
        return null;
    }

    public override bool Delete(Rectangle rectangle)
    {
        var rectToRemove = Find(rectangle);
        if (rectToRemove != null)
        {
            rectangles.Remove(rectToRemove);
            return true;
        }
        return false;
    }

    public override bool Update(Rectangle rectangle)
    {
        var rectToUpdate = Find(rectangle);
        if (rectToUpdate != null)
        {
            Delete(rectToUpdate);
            Insert(rectangle);
            return true;
        }
        return false;
    }

    public override void Dump(int indent)
    {
        Console.WriteLine($"{new string(' ', indent)}Leaf Node - ({Rectangle.X}, {Rectangle.Y}) - {Rectangle.Width}x{Rectangle.Height}");
        foreach (Rectangle rect in rectangles)
        {
            Console.WriteLine($"{new string(' ', indent + 2)}Rectangle at {rect.X}, {rect.Y}: {rect.Width}x{rect.Height}");
        }
    }
}