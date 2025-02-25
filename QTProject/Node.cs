/// <summary>
/// Represents an abstract base class for nodes in the quadtree.
/// </summary>
public abstract class Node
{
    public Rectangle Rectangle { get; set; } // Define the space as a rectangle

    /// <summary>
    /// Initializes a new instance of the <see cref="Node"/> class.
    /// </summary>
    /// <param name="rectangle">The rectangle defining this node's space.</param>
    public Node(Rectangle rectangle)
    {
        Rectangle = rectangle; // Set the rectangle property
    }

    /// <summary>
    /// Inserts a rectangle into this node.
    /// </summary>
    /// <param name="rectangle">The rectangle to insert.</param>
    /// <returns>True if the rectangle was successfully inserted, false otherwise.</returns>
    public abstract Node Insert(Rectangle rectangle);

    /// <summary>
    /// Finds a rectangle at the specified coordinates.
    /// </summary>
    /// <param name="rectangle">The rectangle containing the coordinates.</param>
    /// <returns>The found rectangle, or null if not found.</returns>
    public abstract Rectangle Find(Rectangle rectangle);

    /// <summary>
    /// Deletes a rectangle from this node.
    /// </summary>
    /// <param name="rectangle">The rectangle to delete.</param>
    /// <returns>True if the rectangle was deleted, false otherwise.</returns>
    public abstract bool Delete(Rectangle rectangle);

    /// <summary>
    /// Updates a rectangle in this node.
    /// </summary>
    /// <param name="rectangle">The rectangle with updated dimensions.</param>
    /// <returns>True if the rectangle was updated, false otherwise.</returns>
    public abstract bool Update(Rectangle rectangle);

    /// <summary>
    /// Dumps the structure of the node.
    /// </summary>
    /// <param name="indent">The indentation level for display.</param>
    public abstract void Dump(int indent);
}