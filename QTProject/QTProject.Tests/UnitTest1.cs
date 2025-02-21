using System;
using Xunit;
using QTProject;

public class QuadTreeTests
{
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