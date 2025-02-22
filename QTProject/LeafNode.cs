using System;
using System.Collections.Generic;

/// <summary>
/// Represents a leaf node in the quadtree, storing rectangles directly.
/// </summary>
public class LeafNode : Node
{
    private List<Rectangle> rectangles; // List to store rectangles
    private const int MAX_RECTANGLES = 5; // Maximum rectangles before splitting

    /// <summary>
    /// Initializes a new instance of the <see cref="LeafNode"/> class.
    /// </summary>
    /// <param name="rectangle">The rectangle defining this node's space.</param>
    public LeafNode(Rectangle rectangle) : base(rectangle)
    {
        rectangles = new List<Rectangle>();
    }

    /// <summary>
    /// Inserts a rectangle into this leaf node.
    /// </summary>
    /// <param name="rectangle">The rectangle to insert.</param>
    /// <returns>True if the rectangle was successfully inserted, false if it needs to split.</returns>
    public override bool Insert(Rectangle rectangle)
    {
        if (rectangles.Count < MAX_RECTANGLES)
        {
            rectangles.Add(rectangle);
            return true;
        }
        else
        {
            // BS: You're making two slight mistakes here. Look at what this function does.
            return Split(rectangle); // Split if max rectangles reached
            // BS: After you make the new Internal node, you never added the rectangle argument. Only the old rectangles are in the new node.
        }
    }

    /// <summary>
    /// Splits this leaf node into an internal node and transfers rectangles.
    /// </summary>
    /// <param name="rectangle">The rectangle to insert that caused the split.</param>
    /// <returns>True after splitting and reinserting rectangles.</returns>
    private bool Split(Rectangle rectangle)
    {
        InternalNode parent = new InternalNode(Rectangle); // Create a new internal node
        foreach (Rectangle rect in rectangles)
        {
            parent.Insert(rect); // Reinsert existing rectangles
        }
        parent.Insert(rectangle); // Insert the new rectangle
        // BS: you made the new InternalNode, but you don't actually do anything with it.
        // Replace this leaf node with the new parent in the tree (omitted in this context)
        return true; // Return true after splitting
    }

    /// <summary>
    /// Finds a rectangle at the given coordinates.
    /// </summary>
    /// <param name="rectangle">The rectangle to find (with coordinates only).</param>
    /// <returns>The found rectangle, or null if not found.</returns>
    public override Rectangle Find(Rectangle rectangle)
    {
        foreach (Rectangle rect in rectangles)
        {
            // Check if the rectangle's coordinates match
            if (rect.Contains(rectangle)) // Using Contains method for a better check
                return rect;
        }
        return null; // Return null if not found
    }

    /// <summary>
    /// Deletes a rectangle from this leaf node.
    /// </summary>
    /// <param name="rectangle">The rectangle to delete.</param>
    /// <returns>True if the rectangle was deleted, false otherwise.</returns>
    public override bool Delete(Rectangle rectangle)
    {
        Rectangle rectToRemove = Find(rectangle);
        if (rectToRemove != null)
        {
            rectangles.Remove(rectToRemove); // Remove the found rectangle
            return true;
        }
        return false; // Return false if not found
    }

    /// <summary>
    /// Updates a rectangle in this leaf node.
    /// </summary>
    /// <param name="rectangle">The rectangle to update, with new dimensions.</param>
    /// <returns>True if the rectangle was updated, false otherwise.</returns>
    public override bool Update(Rectangle rectangle)
    {
        Rectangle rectToUpdate = Find(rectangle);
        if (rectToUpdate != null)
        {
            // Update dimensions of the found rectangle
            rectToUpdate.Length = rectangle.Length;
            rectToUpdate.Width = rectangle.Width;
            return true;
        }
        return false; // Return false if not found
    }

    /// <summary>
    /// Dumps the structure of the leaf node.
    /// </summary>
    /// <param name="indent">The indentation level for display.</param>
    public override void Dump(int indent)
    {
        Console.WriteLine($"{new string(' ', indent)}Leaf Node - ({Rectangle.X}, {Rectangle.Y}) - {Rectangle.Length}x{Rectangle.Width}");
        foreach (Rectangle rect in rectangles)
        {
            Console.WriteLine($"{new string(' ', indent + 4)}Rectangle at {rect.X}, {rect.Y}: {rect.Length}x{rect.Width}");
        }
    }
}
