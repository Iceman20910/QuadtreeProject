using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Represents a quadtree for managing rectangles in a 2D space.
/// </summary>
public class QuadTree
{
    private Node root; // Root node of the quadtree

    /// <summary>
    /// Initializes a new instance of the <see cref="QuadTree"/> class.
    /// </summary>
    public QuadTree()
    {
        root = new LeafNode(new Rectangle(0, 0, 100, 100)); // Initialize root as a leaf node
    }

    /// <summary>
    /// Inserts a rectangle into the quadtree.
    /// </summary>
    /// <param name="rectangle">The rectangle to insert.</param>
    public void Insert(Rectangle rectangle)
    {
        // Validate rectangle dimensions
        if (rectangle.Length <= 0 || rectangle.Width <= 0)
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

        if (!root.Insert(rectangle))
        {
            Console.WriteLine($"Error: A rectangle already exists at ({rectangle.X}, {rectangle.Y})");
        }
    }

    /// <summary>
    /// Finds a rectangle at the given coordinates.
    /// </summary>
    /// <param name="rectangle">The rectangle containing the coordinates.</param>
    public void Find(Rectangle rectangle)
    {
        if (!IsInBounds(rectangle))
        {
            Console.WriteLine("Error: Coordinates out of bounds");
            return;
        }

        Rectangle foundRectangle = root.Find(rectangle);
        if (foundRectangle != null)
        {
            Console.WriteLine($"Rectangle at {foundRectangle.X}, {foundRectangle.Y}: {foundRectangle.Length}x{foundRectangle.Width}");
        }
        else
        {
            Console.WriteLine($"Error: No rectangle found at ({rectangle.X}, {rectangle.Y})");
        }
    }

    /// <summary>
    /// Deletes a rectangle from the quadtree.
    /// </summary>
    /// <param name="rectangle">The rectangle to delete.</param>
    public void Delete(Rectangle rectangle)
    {
        if (!IsInBounds(rectangle))
        {
            Console.WriteLine("Error: Coordinates out of bounds");
            return;
        }

        if (!root.Delete(rectangle))
        {
            Console.WriteLine($"Error: No rectangle found at ({rectangle.X}, {rectangle.Y})");
        }
    }

    /// <summary>
    /// Updates a rectangle in the quadtree.
    /// </summary>
    /// <param name="rectangle">The rectangle with updated dimensions.</param>
    public void Update(Rectangle rectangle)
    {
        if (rectangle.Length <= 0 || rectangle.Width <= 0)
        {
            Console.WriteLine("Error: Invalid rectangle dimensions");
            return;
        }

        if (!IsInBounds(rectangle))
        {
            Console.WriteLine("Error: Rectangle out of bounds");
            return;
        }

        if (!root.Update(rectangle))
        {
            Console.WriteLine($"Error: No rectangle found at ({rectangle.X}, {rectangle.Y})");
        }
    }

    /// <summary>
    /// Dumps the structure of the quadtree.
    /// </summary>
    public void Dump()
    {
        root.Dump(0);
    }

    /// <summary>
    /// Processes commands from a specified command file.
    /// </summary>
    /// <param name="filePath">The path to the command file.</param>
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
                    if (parts.Length == 5)
                        Insert(new Rectangle(int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4])));
                    else
                        Console.WriteLine($"Error: Invalid parameters for insert - {trimmedLine}");
                    break;
                case "find":
                    if (parts.Length == 3)
                        Find(new Rectangle(int.Parse(parts[1]), int.Parse(parts[2]), 0, 0)); // Corrected parsing
                    else
                        Console.WriteLine($"Error: Invalid parameters for find - {trimmedLine}");
                    break;
                case "delete":
                    if (parts.Length == 3)
                        Delete(new Rectangle(int.Parse(parts[1]), int.Parse(parts[2]), 0, 0)); // Corrected parsing
                    else
                        Console.WriteLine($"Error: Invalid parameters for delete - {trimmedLine}");
                    break;
                case "update":
                    if (parts.Length == 5)
                        Update(new Rectangle(int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4])));
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
    /// <param name="rectangle">The rectangle to check.</param>
    /// <returns>True if the rectangle is within bounds, false otherwise.</returns>
    private bool IsInBounds(Rectangle rectangle)
    {
        return rectangle.X >= -50 && rectangle.X <= 50 &&
               rectangle.Y >= -50 && rectangle.Y <= 50 &&
               rectangle.X + rectangle.Length <= 50 &&
               rectangle.Y + rectangle.Width <= 50;
    }
}