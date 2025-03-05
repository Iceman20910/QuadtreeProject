using System;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: QTProject <command_file.cmmd>");
            return;
        }

        string commandFilePath = args[0];
        
        try
        {
            QuadTree quadTree = new QuadTree();
            quadTree.ProcessCommands(commandFilePath);
        }
        catch (System.IO.FileNotFoundException)
        {
            Console.WriteLine($"Error: The file '{commandFilePath}' was not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while processing commands: {ex.Message}");
        }
    }
}