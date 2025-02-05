using System;

namespace QTProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Handle command line arguments
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: QTProject <file.cmmd>");
                return;
            }

            string filePath = args[0];
            QuadTree quadTree = new QuadTree();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(' ');
                    switch (parts[0].ToLower())
                    {
                        case "insert":
                            int x = int.Parse(parts[1]);
                            int y = int.Parse(parts[2]);
                            int length = int.Parse(parts[3]);
                            int width = int.Parse(parts[4]);
                            quadTree.Insert(x, y, length, width);
                            break;
                        case "delete":
                            x = int.Parse(parts[1]);
                            y = int.Parse(parts[2]);
                            quadTree.Delete(x, y);
                            break;
                        case "find":
                            x = int.Parse(parts[1]);
                            y = int.Parse(parts[2]);
                            Rectangle? rect = quadTree.Find(x, y);
                            if (rect.HasValue)
                            {
                                Console.WriteLine($"Rectangle at {x}, {y}: {rect.Value.Length}x{rect.Value.Width}");
                            }
                            else
                            {
                                Console.WriteLine($"Nothing is at {x}, {y}");
                            }
                            break;
                        case "update":
                            x = int.Parse(parts[1]);
                            y = int.Parse(parts[2]);
                            length = int.Parse(parts[3]);
                            width = int.Parse(parts[4]);
                            quadTree.Update(x, y, length, width);
                            break;
                        case "dump":
                            quadTree.Dump();
                            break;
                        default:
                            Console.WriteLine($"Invalid command: {line}");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
