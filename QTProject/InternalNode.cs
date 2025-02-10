using System;

public class InternalNode : Node
{
    private Node[] children;

    public InternalNode(int x, int y, int length, int width)
        : base(x, y, length, width)
    {
        children = new Node[4];
    }

    private int GetQuadrantIndex(int x, int y)
    {
        int quadrantX = x < X + Length / 2 ? 0 : 1;
        int quadrantY = y < Y + Width / 2 ? 0 : 2;
        return quadrantX + quadrantY;
    }

    public override bool Insert(Rectangle rectangle)
    {
        int index = GetQuadrantIndex(rectangle.X, rectangle.Y);
        if (children[index] == null)
        {
            int childLength = Length / 2;
            int childWidth = Width / 2;
            int childX = X + (index % 2 == 0 ? 0 : childLength);
            int childY = Y + (index < 2 ? 0 : childWidth);
            children[index] = new LeafNode(childX, childY, childLength, childWidth);
        }
        return children[index].Insert(rectangle);
    }

    public override Rectangle Find(int x, int y)
    {
        int index = GetQuadrantIndex(x, y);
        if (children[index] == null)
            return null;
        return children[index].Find(x, y);
    }

    public override bool Delete(int x, int y)
    {
        int index = GetQuadrantIndex(x, y);
        if (children[index] == null)
            return false;
        return children[index].Delete(x, y);
    }

    public override bool Update(int x, int y, int length, int width)
    {
        int index = GetQuadrantIndex(x, y);
        if (children[index] == null)
            return false;
        return children[index].Update(x, y, length, width);
    }

    public override void Dump(int indent)
    {
        Console.WriteLine($"{new string(' ', indent)}Internal Node - ({X}, {Y}) - {Length}x{Width}");
        foreach (Node child in children)
        {
            if (child != null)
                child.Dump(indent + 4);
        }
    }
}
