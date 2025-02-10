public abstract class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }

    public Node(int x, int y, int length, int width)
    {
        X = x;
        Y = y;
        Length = length;
        Width = width;
    }

    public abstract bool Insert(Rectangle rectangle);
    public abstract Rectangle Find(int x, int y);
    public abstract bool Delete(int x, int y);
    public abstract bool Update(int x, int y, int length, int width);
    public abstract void Dump(int indent);
}
