using Xunit;
using System.IO;

namespace QTProjectTests
{
    public class QuadTreeTests
    {
        [Fact]
        public void Insert_ValidRectangle_Success()
        {
            var quadTree = new QuadTree();
            quadTree.Insert(new Rectangle(10, 10, 5, 5));
            var rect = quadTree.Find(new Rectangle(10, 10, 0, 0));
            Assert.NotNull(rect);
            Assert.Equal(10, rect.X);
            Assert.Equal(10, rect.Y);
            Assert.Equal(5, rect.Height);
            Assert.Equal(5, rect.Width);
        }

        [Fact]
        public void Insert_RectangleOutsideBounds_Fails()
        {
            var quadTree = new QuadTree();
            quadTree.Insert(new Rectangle(200, 200, 10, 10));
            Assert.Null(quadTree.Find(new Rectangle(200, 200, 0, 0)));
        }

        [Fact]
        public void Insert_MultipleRectangles_Success()
        {
            var quadTree = new QuadTree();
            quadTree.Insert(new Rectangle(10, 10, 5, 5));
            quadTree.Insert(new Rectangle(20, 20, 10, 10));
            quadTree.Insert(new Rectangle(-30, -30, 15, 15));
            Assert.NotNull(quadTree.Find(new Rectangle(10, 10, 0, 0))); 
            Assert.NotNull(quadTree.Find(new Rectangle(20, 20, 0, 0))); 
            Assert.NotNull(quadTree.Find(new Rectangle(-30, -30, 0, 0))); 
        }

        [Fact]
        public void Delete_ExistingRectangle_Success()
        {
            var quadTree = new QuadTree();
            quadTree.Insert(new Rectangle(10, 10, 5, 5));
            quadTree.Delete(new Rectangle(10, 10, 0, 0));
            Assert.Null(quadTree.Find(new Rectangle(10, 10, 0, 0))); 
        }

        [Fact]
        public void Delete_NonExistingRectangle_NoEffect()
        {
            var quadTree = new QuadTree();
            quadTree.Delete(new Rectangle(10, 10, 0, 0)); // No exception thrown
            Assert.Null(quadTree.Find(new Rectangle(10, 10, 0, 0))); 
        }

        [Fact]
        public void Update_ExistingRectangle_Success()
        {
            var quadTree = new QuadTree();
            quadTree.Insert(new Rectangle(10, 10, 5, 5));
            quadTree.Update(new Rectangle(10, 10, 10, 10)); // Update to larger dimensions
            var rect = quadTree.Find(new Rectangle(10, 10, 0, 0));
            Assert.NotNull(rect);
            Assert.Equal(10, rect.Height); // Expect height to be updated to 10
            Assert.Equal(10, rect.Width);  // Expect width to be updated to 10
        }

        [Fact]
        public void Update_NonExistingRectangle_NoEffect()
        {
            var quadTree = new QuadTree();
            quadTree.Update(new Rectangle(10, 10, 10, 10)); // No exception thrown
            Assert.Null(quadTree.Find(new Rectangle(10, 10, 0, 0))); 
        }

        [Fact]
        public void Find_ExistingRectangle_Success()
        {
            var quadTree = new QuadTree();
            quadTree.Insert(new Rectangle(10, 10, 5, 5));
            Assert.NotNull(quadTree.Find(new Rectangle(10, 10, 0, 0)));
        }

        [Fact]
        public void Find_NonExistingRectangle_ReturnsNull()
        {
            var quadTree = new QuadTree();
            Assert.Null(quadTree.Find(new Rectangle(10, 10, 0, 0)));
        }

        [Fact]
        public void Dump_EmptyTree_PrintsRootNode()
        {
            var quadTree = new QuadTree();
            var output = new StringWriter();
            Console.SetOut(output);
            quadTree.Dump();
            var expected = "Quadtree Dump:\n\n\tLeaf Node - (0, 0) - 100x100\n"; // Adjusted for expected output
            Assert.Equal(expected, output.ToString());
        }

        [Fact]
        public void Dump_TreeWithRectangles_PrintsStructure()
        {
            var quadTree = new QuadTree();
            quadTree.Insert(new Rectangle(10, 10, 5, 5));
            quadTree.Insert(new Rectangle(20, 20, 10, 10));
            quadTree.Insert(new Rectangle(-30, -30, 15, 15));
            var output = new StringWriter();
            Console.SetOut(output);
            quadTree.Dump();

            var expected = "Quadtree Dump:\n\n\tLeaf Node - (0, 0) - 100x100\n" +
                           "\t\tRectangle at 10, 10: 5x5\n" +
                           "\t\tRectangle at 20, 20: 10x10\n" +
                           "\t\tRectangle at -30, -30: 15x15\n"; // Adjusted for expected output
            Assert.Equal(expected, output.ToString());
        }

        [Fact]
        public void TestInsertOutOfBounds()
        {
            var qt = new QuadTree();
            var rect = new Rectangle(150, 150, 20, 20);
            qt.Insert(rect);
            Assert.Null(qt.Find(new Rectangle(150, 150, 0, 0))); // Should return null
        }

        [Fact]
        public void TestUpdateRectangle()
        {
            var qt = new QuadTree();
            var rect = new Rectangle(10, 10, 20, 20);
            qt.Insert(rect);
            var updatedRect = new Rectangle(10, 10, 30, 30); // Update to new dimensions
            qt.Update(updatedRect);
            var foundRect = qt.Find(new Rectangle(10, 10, 0, 0)); // Using zero dimensions to find
            Assert.NotNull(foundRect);
            Assert.Equal(updatedRect.Height, foundRect.Height);
            Assert.Equal(updatedRect.Width, foundRect.Width);
        }
    }
}