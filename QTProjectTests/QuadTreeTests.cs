using Xunit;

namespace QTProjectTests
{
    public class QuadTreeTests
    {
        [Fact]
        public void Insert_ValidRectangle_Success()
        {
            // Arrange
            var quadTree = new QuadTree();

            // Act
            quadTree.Insert(10, 10, 5, 5);

            // Assert
            var rect = quadTree.Find(10, 10);
            Assert.NotNull(rect);
            Assert.Equal(10, rect.Value.X);
            Assert.Equal(10, rect.Value.Y);
            Assert.Equal(5, rect.Value.Length);
            Assert.Equal(5, rect.Value.Width);
        }

        [Fact]
        public void Insert_RectangleOutsideBounds_Fails()
        {
            // Arrange
            var quadTree = new QuadTree();

            // Act & Assert
            quadTree.Insert(200, 200, 10, 10);
            Assert.Null(quadTree.Find(200, 200));
        }

        [Fact]
        public void Insert_MultipleRectangles_Success()
        {
            // Arrange
            var quadTree = new QuadTree();

            // Act
            quadTree.Insert(10, 10, 5, 5);
            quadTree.Insert(20, 20, 10, 10);
            quadTree.Insert(-30, -30, 15, 15);

            // Assert
            Assert.NotNull(quadTree.Find(10, 10));
            Assert.NotNull(quadTree.Find(20, 20));
            Assert.NotNull(quadTree.Find(-30, -30));
        }

        [Fact]
        public void Delete_ExistingRectangle_Success()
        {
            // Arrange
            var quadTree = new QuadTree();
            quadTree.Insert(10, 10, 5, 5);

            // Act
            quadTree.Delete(10, 10);

            // Assert
            Assert.Null(quadTree.Find(10, 10));
        }

        [Fact]
        public void Delete_NonExistingRectangle_NoEffect()
        {
            // Arrange
            var quadTree = new QuadTree();

            // Act & Assert
            quadTree.Delete(10, 10); // No exception thrown
        }

        [Fact]
        public void Update_ExistingRectangle_Success()
        {
            // Arrange
            var quadTree = new QuadTree();
            quadTree.Insert(10, 10, 5, 5);

            // Act
            quadTree.Update(10, 10, 10, 10);

            // Assert
            var rect = quadTree.Find(10, 10);
            Assert.NotNull(rect);
            Assert.Equal(10, rect.Value.Length);
            Assert.Equal(10, rect.Value.Width);
        }

        [Fact]
        public void Update_NonExistingRectangle_NoEffect()
        {
            // Arrange
            var quadTree = new QuadTree();

            // Act & Assert
            quadTree.Update(10, 10, 10, 10); // No exception thrown
        }

        [Fact]
        public void Find_ExistingRectangle_Success()
        {
            // Arrange
            var quadTree = new QuadTree();
            quadTree.Insert(10, 10, 5, 5);

            // Act & Assert
            Assert.NotNull(quadTree.Find(10, 10));
        }

        [Fact]
        public void Find_NonExistingRectangle_ReturnsNull()
        {
            // Arrange
            var quadTree = new QuadTree();

            // Act & Assert
            Assert.Null(quadTree.Find(10, 10));
        }

        [Fact]
        public void Dump_EmptyTree_PrintsRootNode()
        {
            // Arrange
            var quadTree = new QuadTree();
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            quadTree.Dump();

            // Assert
            var expected = "Quadtree Dump:\r\n\tLeafNode - (0, 0) - 100x100\r\n";
            Assert.Equal(expected, output.ToString());
        }

        [Fact]
        public void Dump_TreeWithRectangles_PrintsStructure()
        {
            // Arrange
            var quadTree = new QuadTree();
            quadTree.Insert(10, 10, 5, 5);
            quadTree.Insert(20, 20, 10, 10);
            quadTree.Insert(-30, -30, 15, 15);
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            quadTree.Dump();

            // Assert
            // TODO: Add expected output string
        }
    }
}
