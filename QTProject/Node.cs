namespace QTProject
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }

        public Node(int x, int y, int length, int width)
        {
            X = x;
            Y = y;
            Length = length;
            Width = width;
        }
    }

    public class InternalNode : Node
    {
        public Node[] Children { get; set; }

        public InternalNode(int x, int y, int length, int width) 
            : base(x, y, length, width)
        {
            Children = new Node[4];
        }
    }

    public class LeafNode : Node
    {
        public List<Rectangle> Rectangles { get; set; }

        public LeafNode(int x, int y, int length, int width)
            : base(x, y, length, width)
        {
            Rectangles = new List<Rectangle>();
        }
    }
}
