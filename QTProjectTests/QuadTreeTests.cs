using Xunit; // BS: remove this. Your .csproj already has it on lines 25-27

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
            quadTree.Insert(new Rectangle(10, 10, 5, 5));

            // Assert
            var rect = quadTree.Find(new Rectangle(10, 10, 0, 0)); // Adjusted to use Rectangle BS: Find returns void, so you can't assign to it. Update the return type.
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

            // Act
            quadTree.Insert(new Rectangle(200, 200, 10, 10));

            // Assert
            Assert.Null(quadTree.Find(new Rectangle(200, 200, 0, 0))); // Corrected syntax
        }

        [Fact]
        public void Insert_MultipleRectangles_Success()
        {
            // Arrange
            var quadTree = new QuadTree();

            // Act
            quadTree.Insert(new Rectangle(10, 10, 5, 5));
            quadTree.Insert(new Rectangle(20, 20, 10, 10));
            quadTree.Insert(new Rectangle(-30, -30, 15, 15));

            // Assert
            Assert.NotNull(quadTree.Find(new Rectangle(10, 10, 0, 0))); // Corrected syntax
            Assert.NotNull(quadTree.Find(new Rectangle(20, 20, 0, 0))); // Corrected syntax
            Assert.NotNull(quadTree.Find(new Rectangle(-30, -30, 0, 0))); // Corrected syntax
        }

        [Fact]
        public void Delete_ExistingRectangle_Success()
        {
            // Arrange
            var quadTree = new QuadTree();
            quadTree.Insert(new Rectangle(10, 10, 5, 5));

            // Act
            quadTree.Delete(new Rectangle(10, 10, 0, 0));

            // Assert
            Assert.Null(quadTree.Find(new Rectangle(10, 10, 0, 0))); // Corrected syntax
        }

        [Fact]
        public void Delete_NonExistingRectangle_NoEffect()
        {
            // Arrange
            var quadTree = new QuadTree();

            // Act
            quadTree.Delete(new Rectangle(10, 10, 0, 0)); // No exception thrown

            // Assert
            Assert.Null(quadTree.Find(new Rectangle(10, 10, 0, 0))); // Corrected syntax and clarity
        }

        [Fact]
        public void Update_ExistingRectangle_Success()
        {
            // Arrange
            var quadTree = new QuadTree();
            quadTree.Insert(new Rectangle(10, 10, 5, 5));

            // Act
            quadTree.Update(new Rectangle(10, 10, 10, 10));

            // Assert
            var rect = quadTree.Find(new Rectangle(10, 10, 0, 0));
            Assert.NotNull(rect);
            Assert.Equal(10, rect.Length);
            Assert.Equal(10, rect.Width);
        }

        [Fact]
        public void Update_NonExistingRectangle_NoEffect()
        {
            // Arrange
            var quadTree = new QuadTree();

            // Act
            quadTree.Update(new Rectangle(10, 10, 10, 10)); // No exception thrown

            // Assert
            Assert.Null(quadTree.Find(new Rectangle(10, 10, 0, 0))); // Corrected syntax
        }

        [Fact]
        public void Find_ExistingRectangle_Success()
        {
            // Arrange
            var quadTree = new QuadTree();
            quadTree.Insert(new Rectangle(10, 10, 5, 5));

            // Act & Assert
            Assert.NotNull(quadTree.Find(new Rectangle(10, 10, 0, 0)));
        }

        [Fact]
        public void Find_NonExistingRectangle_ReturnsNull()
        {
            // Arrange
            var quadTree = new QuadTree();

            // Act & Assert
            Assert.Null(quadTree.Find(new Rectangle(10, 10, 0, 0)));
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
            quadTree.Insert(new Rectangle(10, 10, 5, 5));
            quadTree.Insert(new Rectangle(20, 20, 10, 10));
            quadTree.Insert(new Rectangle(-30, -30, 15, 15));
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            quadTree.Dump();

            // Assert
            // TODO: Add expected output string based on Dump implementation
            // Example: 
            // var expected = "Quadtree Dump:\r\n\t..."; // Define the expected output
            // Assert.Equal(expected, output.ToString());
        }

        [Fact]
        public void TestInsertValidRectangle()
        {
            // Arrange
            QuadTree qt = new QuadTree();
            Rectangle rect = new Rectangle(10, 10, 20, 20);

            // Act
            qt.Insert(rect);

            // Assert
            var foundRect = qt.Find(new Rectangle(10, 10, 0, 0)); // Using zero dimensions to find
            Assert.NotNull(foundRect);
            Assert.Equal(rect.X, foundRect.X);
            Assert.Equal(rect.Y, foundRect.Y);
            Assert.Equal(rect.Length, foundRect.Length);
            Assert.Equal(rect.Width, foundRect.Width);
        }

        [Fact]
        public void TestFindRectangle()
        {
            // Arrange
            QuadTree qt = new QuadTree();
            Rectangle rect = new Rectangle(10, 10, 20, 20);
            qt.Insert(rect);

            // Act
            var foundRect = qt.Find(new Rectangle(10, 10, 0, 0)); // Using zero dimensions to find

            // Assert
            Assert.NotNull(foundRect);
            Assert.Equal(rect.X, foundRect.X);
            Assert.Equal(rect.Y, foundRect.Y);
            Assert.Equal(rect.Length, foundRect.Length);
            Assert.Equal(rect.Width, foundRect.Width);
        }

        [Fact]
        public void TestDeleteRectangle()
        {
            // Arrange
            QuadTree qt = new QuadTree();
            Rectangle rect = new Rectangle(10, 10, 20, 20);
            qt.Insert(rect);

            // Act
            qt.Delete(new Rectangle(10, 10, 0, 0)); // Using zero dimensions to delete

            // Assert
            Assert.Null(qt.Find(new Rectangle(10, 10, 0, 0))); // Corrected syntax
        }

        [Fact]
        public void TestInsertOutOfBounds()
        {
            // Arrange
            QuadTree qt = new QuadTree();
            Rectangle rect = new Rectangle(150, 150, 20, 20);

            // Act
            qt.Insert(rect); // Expecting an error message or no insertion

            // Assert
            Assert.Null(qt.Find(new Rectangle(150, 150, 0, 0))); // Corrected syntax
        }

        [Fact]
        public void TestUpdateRectangle()
        {
            // Arrange
            QuadTree qt = new QuadTree();
            Rectangle rect = new Rectangle(10, 10, 20, 20);
            qt.Insert(rect);

            // Act
            Rectangle updatedRect = new Rectangle(10, 10, 30, 30);
            qt.Update(updatedRect); // Update should be by the same position

            // Assert
            var foundRect = qt.Find(new Rectangle(10, 10, 0, 0)); // Using zero dimensions to find
            Assert.NotNull(foundRect);
            Assert.Equal(updatedRect.Length, foundRect.Length);
            Assert.Equal(updatedRect.Width, foundRect.Width);
        }

    }
}
