using System.Collections.Generic;

public class LeafNode : Node
{
    private List<Rectangle> rectangles;
    private const int MAX_RECTANGLES = 5;

    public LeafNode(int x, int y, int length, int width)
        : base(x, y, length, width)
    {
        rectangles = new List<Rectangle>();
    }

    public override bool Insert(Rectangle rectangle)
    {
        if (rectangles.Count < MAX_RECTANGLES)
        {
            rectangles.Add(rectangle);
            return true;
        }
        else
        {
            return Split(rectangle);
        }
    }

    private bool Split(Rectangle rectangle)
    {
        InternalNode parent = new InternalNode(X, Y, Length, Width);
        foreach (Rectangle rect in rectangles)
        {
            parent.Insert(rect);
        }
        parent.Insert(rectangle);
        return true;
    }

    public override Rectangle Find(int x, int y)
    {
        foreach (Rectangle rect in rectangles)
        {
            if (rect.X == x && rect.Y == y)
                return rect;
        }
        return null;
    }

    public override bool Delete(int x, int y)
    {
        Rectangle rectToRemove = Find(x, y);
        if (rectToRemove != null)
        {
            rectangles.Remove(rectToRemove);
            return true;
        }
        return false;
    }

    public override bool Update(int x, int y, int length, int width)
    {
        Rectangle rectToUpdate = Find(x, y);
        if (rectToUpdate != null)
        {
            rectToUpdate.Length = length;
            rectToUpdate.Width = width;
            return true;
        }
        return false;
    }

    public override void Dump(int indent)
    {
        Console.WriteLine($"{new string(' ', indent)}Leaf Node - ({X}, {Y}) - {Length}x{Width}");
        foreach (Rectangle rect in rectangles)
        {
            Console.WriteLine($"{new string(' ', indent + 4)}Rectangle at {rect.X}, {rect.Y}: {rect.Length}x{rect.Width}");
        }
    }
}
