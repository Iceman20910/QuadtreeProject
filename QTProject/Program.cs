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
        QuadTree quadTree = new QuadTree();
        quadTree.ProcessCommands(commandFilePath);
    }
}