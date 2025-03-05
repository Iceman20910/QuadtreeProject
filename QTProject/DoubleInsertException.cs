/// <summary>
/// Custom exception for handling double insertions in the quadtree.
/// </summary>
public class DoubleInsertException : Exception
{
    public DoubleInsertException(string message) : base(message) { }
}