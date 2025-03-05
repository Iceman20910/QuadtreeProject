/// <summary>
/// Abstract class representing a node in the quadtree.
/// </summary>
public abstract class Node
{
    protected Rectangle Rectangle;

    protected Node(Rectangle rectangle)
    {
        Rectangle = rectangle;
    }

    public abstract Node Insert(Rectangle rectangle);
    public abstract Rectangle? Find(Rectangle rectangle);
    public abstract bool Delete(Rectangle rectangle);
    public abstract bool Update(Rectangle rectangle);
    public abstract void Dump(int indent);
}