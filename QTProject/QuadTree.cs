using System;
using System.Collections.Generic;

namespace QTProject
{
    public class QuadTree
    {
        private Node root;

        public QuadTree()
        {
            root = new LeafNode(0, 0, 100, 100);
        }

        public void Insert(int x, int y, int length, int width)
        {
            InsertRecursive(root, x, y, length, width);
        }

        private void InsertRecursive(Node node, int x, int y, int length, int width)
        {
            // Check if rectangle fits inside this node
            if (x >= node.X && x + length <= node.X + node.Length &&
                y >= node.Y && y + width <= node.Y + node.Width)
            {
                // If this is a leaf node
                if (node is LeafNode leafNode)
                {
                    // If leaf node has less than 5 rectangles, insert here
                    if (leafNode.Rectangles == null)
                    {
                        leafNode.Rectangles = new List<Rectangle>();
                    }
                    if (leafNode.Rectangles.Count < 5)
                    {
                        leafNode.Rectangles.Add(new Rectangle
                        {
                            X = x,
                            Y = y, 
                            Length = length,
                            Width = width
                        });
                        return;
                    }
                    // Otherwise, split the leaf node
                    else
                    {
                        SplitNode(leafNode);
                        InsertRecursive(leafNode, x, y, length, width);
                    }
                }
                // If this is an internal node
                else if (node is InternalNode internalNode)
                {
                    // Find the child node that contains the rectangle
                    Node childNode = GetChildNode(internalNode, x, y);
                    InsertRecursive(childNode, x, y, length, width);
                }
            }
            else
            {
                // Rectangle does not fit inside this node, cannot insert
                Console.WriteLine($"Cannot insert rectangle at ({x}, {y}) with length {length} and width {width}");
            }
        }

        private Node GetChildNode(InternalNode node, int x, int y)
        {
            // Determine which quadrant the point (x, y) lies in
            int quadrant = 0;
            if (x >= node.X + node.Length / 2)
            {
                quadrant += 1;
            }
            if (y >= node.Y + node.Width / 2)
            {
                quadrant += 2;
            }

            // Return the child node for that quadrant
            return node.Children[quadrant];
        }

        public void Delete(int x, int y)
        {
            DeleteRecursive(root, x, y);
        }

        private void DeleteRecursive(Node node, int x, int y)
        {
            // Check if point is inside this node's space
            if (x >= node.X && x < node.X + node.Length &&
                y >= node.Y && y < node.Y + node.Width)
            {
                // If this is a leaf node
                if (node is LeafNode leafNode)
                {
                    // Find and remove the rectangle if it exists
                    Rectangle? rectToRemove = leafNode.Rectangles.Find(r => r.X == x && r.Y == y);
                    if (rectToRemove.HasValue)
                    {
                        leafNode.Rectangles.Remove(rectToRemove.Value);
                    }
                    else
                    {
                        Console.WriteLine($"Nothing to delete at ({x}, {y})");
                    }
                }
                // If this is an internal node
                else if (node is InternalNode internalNode)
                {
                    // Find the child node that contains the point
                    Node childNode = GetChildNode(internalNode, x, y);
                    DeleteRecursive(childNode, x, y);
                }
            }
            else
            {
                // Point is not inside this node's space
                Console.WriteLine($"Nothing to delete at ({x}, {y})");
            }
        }

        public Rectangle? Find(int x, int y)
        {
            return FindRecursive(root, x, y);
        }

        private Rectangle? FindRecursive(Node node, int x, int y)
        {
            // Check if point is inside this node's space
            if (x >= node.X && x < node.X + node.Length &&
                y >= node.Y && y < node.Y + node.Width)
            {
                // If this is a leaf node
                if (node is LeafNode leafNode)
                {
                    // Find the rectangle if it exists
                    return leafNode.Rectangles.Find(r => r.X == x && r.Y == y);
                }
                // If this is an internal node
                else if (node is InternalNode internalNode)
                {
                    // Find the child node that contains the point
                    Node childNode = GetChildNode(internalNode, x, y);
                    return FindRecursive(childNode, x, y);
                }
            }

            // Point is not inside this node's space
            return null;
        }

        public void Update(int x, int y, int newLength, int newWidth)
        {
            UpdateRecursive(root, x, y, newLength, newWidth);
        }

        private void UpdateRecursive(Node node, int x, int y, int newLength, int newWidth)
        {
            // Check if point is inside this node's space
            if (x >= node.X && x < node.X + node.Length &&
                y >= node.Y && y < node.Y + node.Width)
            {
                // If this is a leaf node
                if (node is LeafNode leafNode)
                {
                    // Find and update the rectangle if it exists
                    Rectangle? rectToUpdate = leafNode.Rectangles.Find(r => r.X == x && r.Y == y);
                    int index = leafNode.Rectangles.FindIndex(r => r.X == x && r.Y == y);
                    if (index != -1)
                    {
                        leafNode.Rectangles[index] = new Rectangle(x, y, newLength, newWidth);
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Nothing to update at ({x}, {y})");
                    }
                }
                // If this is an internal node
                else if (node is InternalNode internalNode)
                {
                    // Find the child node that contains the point
                    Node childNode = GetChildNode(internalNode, x, y);
                    UpdateRecursive(childNode, x, y, newLength, newWidth);
                }
            }
            else
            {
                // Point is not inside this node's space
                Console.WriteLine($"Nothing to update at ({x}, {y})");
            }
        }

        public void Dump()
        {
            Console.WriteLine("Quadtree Dump:");
            DumpRecursive(root, 0);
        }

        private void DumpRecursive(Node node, int level)
        {
            string indent = new string('\t', level);

            if (node is LeafNode leafNode)
            {
                Console.WriteLine($"{indent}LeafNode - ({node.X}, {node.Y}) - {node.Length}x{node.Width}");
                if (leafNode.Rectangles != null)
                {
                    foreach (var rect in leafNode.Rectangles)
                    {
                        Console.WriteLine($"{indent}\trectangle - ({rect.X}, {rect.Y}) - {rect.Length}x{rect.Width}");
                    }
                }
            }
            else if (node is InternalNode internalNode)
            {
                Console.WriteLine($"{indent}InternalNode - ({node.X}, {node.Y}) - {node.Length}x{node.Width}");
                for (int i = 0; i < 4; i++)
                {
                    if (internalNode.Children[i] != null)
                    {
                        DumpRecursive(internalNode.Children[i], level + 1);
                    }
                }
            }
        }

        private void SplitNode(LeafNode leafNode)
        {
            // Create a new internal node at the same position as the leaf node
            InternalNode internalNode = new InternalNode(leafNode.X, leafNode.Y, leafNode.Length, leafNode.Width);

            // Create 4 new leaf nodes as children of the internal node
            int childLength = leafNode.Length / 2;
            int childWidth = leafNode.Width / 2;

            internalNode.Children[0] = new LeafNode(leafNode.X, leafNode.Y, childLength, childWidth);
            internalNode.Children[1] = new LeafNode(leafNode.X + childLength, leafNode.Y, childLength, childWidth);
            internalNode.Children[2] = new LeafNode(leafNode.X, leafNode.Y + childWidth, childLength, childWidth);
            internalNode.Children[3] = new LeafNode(leafNode.X + childLength, leafNode.Y + childWidth, childLength, childWidth);

            // Move rectangles from the original leaf node to the appropriate child leaf nodes
            foreach (var rect in leafNode.Rectangles)
            {
                Node childNode = GetChildNode(internalNode, rect.X, rect.Y);
                if (childNode is LeafNode childLeafNode)
                {
                    childLeafNode.Rectangles.Add(rect);
                }
            }

            // Replace the original leaf node with the new internal node
            if (leafNode == root)
            {
                root = internalNode;
            }
            else
            {
                // Find the parent node and replace the child reference
                Node parentNode = FindParentNode(root, leafNode);
                if (parentNode is InternalNode parentInternalNode)
                {
                    int childIndex = GetChildIndex(parentInternalNode, leafNode);
                    parentInternalNode.Children[childIndex] = internalNode;
                }
            }
        }

        private Node FindParentNode(Node node, Node childNode)
        {
            if (node is InternalNode internalNode)
            {
                foreach (var child in internalNode.Children)
                {
                    if (child == childNode)
                    {
                        return internalNode;
                    }
                    else if (child != null)
                    {
                        Node parentNode = FindParentNode(child, childNode);
                        if (parentNode != null)
                        {
                            return parentNode;
                        }
                    }
                }
            }

            return null;
        }

        private int GetChildIndex(InternalNode parentNode, Node childNode)
        {
            for (int i = 0; i < 4; i++)
            {
                if (parentNode.Children[i] == childNode)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
