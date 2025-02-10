using System;
using System.Collections.Generic;
using System.IO;

public class QuadTree
{
    private Node root;

    public QuadTree()
    {
        root = new InternalNode(0, 0, 100, 100);
    }

    public void Insert(int x, int y, int length, int width)
    {
        if (length <= 0 || width <= 0)
        {
            Console.WriteLine("Error: Invalid rectangle dimensions");
            return;
        }

        if (x < -50 || x > 50 || y < -50 || y > 50 || x + length > 50 || y + width > 50)
        {
            Console.WriteLine("Error: Rectangle out of bounds");
            return;
        }

        Rectangle rectangle = new Rectangle(x, y, length, width);
        if (!root.Insert(rectangle))
        {
            Console.WriteLine($"Error: A rectangle already exists at ({x}, {y})");
        }
    }

    public void Find(int x, int y)
    {
        if (x < -50 || x > 50 || y < -50 || y > 50)
        {
            Console.WriteLine("Error: Coordinates out of bounds");
            return;
        }

        Rectangle rectangle = root.Find(x, y);
        if (rectangle != null)
        {
            Console.WriteLine($"Rectangle at {x}, {y}: {rectangle.Length}x{rectangle.Width}");
        }
        else
        {
            Console.WriteLine($"Error: No rectangle found at ({x}, {y})");
        }
    }

    public void Delete(int x, int y)
    {
        if (x < -50 || x > 50 || y < -50 || y > 50)
        {
            Console.WriteLine("Error: Coordinates out of bounds");
            return;
        }

        if (!root.Delete(x, y))
        {
            Console.WriteLine($"Error: No rectangle found at ({x}, {y})");
        }
    }

    public void Update(int x, int y, int length, int width)
    {
        if (length <= 0 || width <= 0)
        {
            Console.WriteLine("Error: Invalid rectangle dimensions");
            return;
        }

        if (x < -50 || x > 50 || y < -50 || y > 50 || x + length > 50 || y + width > 50)
        {
            Console.WriteLine("Error: Rectangle out of bounds");
            return;
        }

        if (!root.Update(x, y, length, width))
        {
            Console.WriteLine($"Error: No rectangle found at ({x}, {y})");
        }
    }

    public void Dump()
    {
        root.Dump(0);
    }

    public void ProcessCommands(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        Parallel.ForEach(lines, line =>
        {
            string[] parts = line.Split(' ');
            switch (parts[0].ToLower())
            {
                case "insert":
                    Insert(int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]));
                    break;
                case "find":
                    Find(int.Parse(parts[1]), int.Parse(parts[2]));
                    break;
                case "delete":
                    Delete(int.Parse(parts[1]), int.Parse(parts[2]));
                    break;
                case "update":
                    Update(int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]));
                    break;
                case "dump":
                    Dump();
                    break;
                default:
                    Console.WriteLine($"Error: Invalid command - {line}");
                    break;
            }
        });
    }
}
