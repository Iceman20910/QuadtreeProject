using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Represents an internal node in the quadtree.
/// </summary>
public class InternalNode : Node
{
    private Node[] children;

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalNode"/> class.
    /// </summary>
    /// <param name="rectangle">The rectangle defining this node's space.</param>
    public InternalNode(Rectangle rectangle) : base(rectangle)
    {
        children = new Node[4];

        // Calculate quadrant sizes
        int halfLength = rectangle.Length / 2;
        int halfWidth = rectangle.Width / 2;

        // Initialize child nodes for each quadrant
        children[0] = new LeafNode(new Rectangle(rectangle.X, rectangle.Y, halfLength, halfWidth)); // Top-left
        children[1] = new LeafNode(new Rectangle(rectangle.X + halfLength, rectangle.Y, halfLength, halfWidth)); // Top-right
        children[2] = new LeafNode(new Rectangle(rectangle.X, rectangle.Y + halfWidth, halfLength, halfWidth)); // Bottom-left
        children[3] = new LeafNode(new Rectangle(rectangle.X + halfLength, rectangle.Y + halfWidth, halfLength, halfWidth)); // Bottom-right
    }

    private int GetQuadrantIndex(Rectangle rectangle)
    {
        // Calculate which quadrant the rectangle belongs to
        int quadrantX = rectangle.X < Rectangle.X + Rectangle.Length / 2 ? 0 : 1;
        int quadrantY = rectangle.Y < Rectangle.Y + Rectangle.Width / 2 ? 0 : 2;
        return quadrantX + quadrantY;
    }

    /// <summary>
    /// Inserts a rectangle into the appropriate child node.
    /// </summary>
    /// <param name="rectangle">The rectangle to insert.</param>
    /// <returns>True if the rectangle was successfully inserted, false otherwise.</returns>
    public override bool Insert(Rectangle rectangle)
    {
        int index = GetQuadrantIndex(rectangle);
        // Ensure the child node exists
        if (children[index] == null)
        {
            // Create a new LeafNode if it doesn't exist
            children[index] = new LeafNode(new Rectangle(
                index % 2 == 0 ? Rectangle.X : Rectangle.X + Rectangle.Length / 2,
                index < 2 ? Rectangle.Y : Rectangle.Y + Rectangle.Width / 2,
                Rectangle.Length / 2,
                Rectangle.Width / 2
            ));
        }
        return children[index].Insert(rectangle);
    }

    /// <summary>
    /// Finds a rectangle at the given coordinates.
    /// </summary>
    /// <param name="rectangle">The rectangle to find.</param>
    /// <returns>The found rectangle, or null if not found.</returns>
    public override Rectangle Find(Rectangle rectangle)
    {
        int index = GetQuadrantIndex(rectangle);
        if (children[index] == null)
            return null;
        return children[index].Find(rectangle);
    }

    /// <summary>
    /// Deletes a rectangle from the quadtree.
    /// </summary>
    /// <param name="rectangle">The rectangle to delete.</param>
    /// <returns>True if the rectangle was deleted, false otherwise.</returns>
    public override bool Delete(Rectangle rectangle)
    {
        int index = GetQuadrantIndex(rectangle);
        if (children[index] == null)
            return false;
        return children[index].Delete(rectangle);
    }

    /// <summary>
    /// Updates a rectangle in the quadtree.
    /// </summary>
    /// <param name="rectangle">The rectangle to update.</param>
    /// <returns>True if the rectangle was updated, false otherwise.</returns>
    public override bool Update(Rectangle rectangle)
    {
        int index = GetQuadrantIndex(rectangle);
        if (children[index] == null)
            return false;
        return children[index].Update(rectangle);
    }

    /// <summary>
    /// Dumps the structure of the internal node.
    /// </summary>
    /// <param name="indent">The indentation level for display.</param>
    public override void Dump(int indent)
    {
        Console.WriteLine($"{new string(' ', indent)}Internal Node - ({Rectangle.X}, {Rectangle.Y}) - {Rectangle.Length}x{Rectangle.Width}");
        foreach (Node child in children)
        {
            if (child != null)
                child.Dump(indent + 4);
        }
    }
}