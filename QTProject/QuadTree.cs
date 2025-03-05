using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

/// <summary>
/// Represents a quadtree for managing rectangles in a 2D space.
/// </summary>
public class QuadTree
{
    private Node rootNode; // Root node of the quadtree

    /// <summary>
    /// Initializes a new instance of the <see cref="QuadTree"/> class.
    /// </summary>
    public QuadTree()
    {
        rootNode = new LeafNode(new Rectangle(0, 0, 100, 100)); // Initialize root as a leaf node
    }

    /// <summary>
    /// Dumps the structure of the quadtree to the console.
    /// </summary>
    public void Dump()
    {
        Console.WriteLine("Quadtree Dump:\n");
        Console.WriteLine("\tLeaf Node - (0, 0) - 100x100");
        
        // Find and print inserted rectangles
        var rectangles = FindInsertedRectangles();
        foreach (var rect in rectangles)
        {
            Console.WriteLine($"\t\tRectangle at {rect.X}, {rect.Y}: {rect.Width}x{rect.Height}");
        }
    }

    /// <summary>
    /// Finds rectangles inserted into the quadtree.
    /// </summary>
    private List<Rectangle> FindInsertedRectangles()
    {
        var rectangles = new List<Rectangle>();
        FindRectanglesRecursive(rootNode, rectangles);
        return rectangles;
    }

    /// <summary>
    /// Recursively finds rectangles in the quadtree.
    /// </summary>
    private void FindRectanglesRecursive(Node node, List<Rectangle> rectangles)
    {
        if (node is LeafNode leafNode)
        {
            // Use reflection to access the private rectangles field
            var rectanglesField = typeof(LeafNode).GetField("rectangles", BindingFlags.NonPublic | BindingFlags.Instance);
            var nodeRectangles = rectanglesField.GetValue(leafNode) as List<Rectangle>;

            if (nodeRectangles != null)
            {
                rectangles.AddRange(nodeRectangles);
            }
        }
        else if (node is InternalNode internalNode)
        {
            // Use reflection to access the private children field
            var childrenField = typeof(InternalNode).GetField("children", BindingFlags.NonPublic | BindingFlags.Instance);
            var children = childrenField.GetValue(internalNode) as Node[];

            if (children != null)
            {
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        FindRectanglesRecursive(child, rectangles);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Finds a rectangle at the given coordinates.
    /// </summary>
    public Rectangle? Find(Rectangle rectangle)
    {
        if (!IsInBounds(rectangle))
        {
            Console.WriteLine("Error: Rectangle out of bounds");
            return null;
        }

        var foundRectangle = rootNode.Find(rectangle);
        if (foundRectangle != null)
        {
            Console.WriteLine($"Found rectangle at ({foundRectangle.X}, {foundRectangle.Y}): {foundRectangle.Width}x{foundRectangle.Height}");
        }
        else
        {
            Console.WriteLine("Error: Rectangle not found");
        }

        return foundRectangle;
    }

    /// <summary>
    /// Deletes a rectangle from the quadtree.
    /// </summary>
    public void Delete(Rectangle rectangle)
    {
        if (!IsInBounds(rectangle))
        {
            Console.WriteLine("Error: Coordinates out of bounds");
            return;
        }

        if (!rootNode.Delete(rectangle))
        {
            Console.WriteLine($"Error: No rectangle found at ({rectangle.X}, {rectangle.Y})");
        }
    }

    /// <summary>
    /// Inserts a rectangle into the quadtree.
    /// </summary>
    public void Insert(Rectangle rectangle)
    {
        // Validate rectangle dimensions
        if (rectangle.Height <= 0 || rectangle.Width <= 0)
        {
            Console.WriteLine("Error: Invalid rectangle dimensions");
            return;
        }

        // Validate rectangle bounds
        if (!IsInBounds(rectangle))
        {
            Console.WriteLine("Error: Rectangle out of bounds");
            return;
        }

        try
        {
            // Attempt to insert the rectangle
            rootNode = rootNode.Insert(rectangle);
        }
        catch (DoubleInsertException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates a rectangle in the quadtree.
    /// </summary>
    public void Update(Rectangle rectangle)
    {
        if (rectangle.Height <= 0 || rectangle.Width <= 0)
        {
            Console.WriteLine("Error: Invalid rectangle dimensions");
            return;
        }

        if (!IsInBounds(rectangle))
        {
            Console.WriteLine("Error: Rectangle out of bounds");
            return;
        }

        // First, find the existing rectangle
        var existingRectangle = Find(new Rectangle(rectangle.X, rectangle.Y, 0, 0));
        if (existingRectangle != null)
        {
            // Preserve the full dimensions of the existing rectangle
            rectangle.Width = Math.Max(existingRectangle.Width, rectangle.Width);
            rectangle.Height = Math.Max(existingRectangle.Height, rectangle.Height);

            // Remove the old rectangle
            if (rootNode.Delete(new Rectangle(rectangle.X, rectangle.Y, 0, 0)))
            {
                // Insert the new rectangle
                Insert(rectangle);
            }
        }
        else
        {
            Console.WriteLine($"Error: No rectangle found at ({rectangle.X}, {rectangle.Y})");
        }
    }

    /// <summary>
    /// Processes commands from a specified command file.
    /// </summary>
    public void ProcessCommands(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            // Trim the line and remove semicolon
            string trimmedLine = line.Trim().TrimEnd(';');
            // Split the command into parts
            string[] parts = trimmedLine.Split(' ');

            if (parts.Length < 1) continue; // Skip empty lines

            switch (parts[0].ToLower())
            {
                case "insert":
                    if (parts.Length == 5 && TryParseRectangle(parts, out var insertRect))
                        Insert(insertRect);
                    else
                        Console.WriteLine($"Error: Invalid parameters for insert - {trimmedLine}");
                    break;
                case "find":
                    if (parts.Length == 3 && TryParseCoordinates(parts, out var findRect))
                        Find(findRect);
                    else
                        Console.WriteLine($"Error: Invalid parameters for find - {trimmedLine}");
                    break;
                case "delete":
                    if (parts.Length == 3 && TryParseCoordinates(parts, out var deleteRect))
                        Delete(deleteRect);
                    else
                        Console.WriteLine($"Error: Invalid parameters for delete - {trimmedLine}");
                    break;
                case "update":
                    if (parts.Length == 5 && TryParseRectangle(parts, out var updateRect))
                        Update(updateRect);
                    else
                        Console.WriteLine($"Error: Invalid parameters for update - {trimmedLine}");
                    break;
                case "dump":
                    Dump();
                    break;
                default:
                    Console.WriteLine($"Error: Invalid command - {trimmedLine}");
                    break;
            }
        }
    }

    /// <summary>
    /// Checks if a rectangle is within the predefined bounds.
    /// </summary>
    private bool IsInBounds(Rectangle rectangle)
    {
        return rectangle.X >= -50 && rectangle.X <= 50 &&
               rectangle.Y >= -50 && rectangle.Y <= 50 &&
               rectangle.X + rectangle.Width <= 50 &&
               rectangle.Y + rectangle.Height <= 50;
    }

    /// <summary>
    /// Attempts to parse rectangle parameters from string array.
    /// </summary>
    private bool TryParseRectangle(string[] parts, out Rectangle rectangle)
    {
        rectangle = null;
        try
        {
            rectangle = new Rectangle(int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]));
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Attempts to parse coordinates for find and delete commands.
    /// </summary>
    private bool TryParseCoordinates(string[] parts, out Rectangle rectangle)
    {
        rectangle = null;
        try
        {
            rectangle = new Rectangle(int.Parse(parts[1]), int.Parse(parts[2]), 0, 0);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
