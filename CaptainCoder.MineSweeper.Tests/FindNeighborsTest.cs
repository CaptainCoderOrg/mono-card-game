using CaptainCoder.Core;
namespace CaptainCoder.MineSweeper.Tests;

public class FindNeighborsTest
{

    [Fact]
    public void TestTopLeft()
    {
        Board board5x10 = new(5, 10);

        // Find the neighbors of the top left position
        List<Position> neighbors = board5x10.FindNeighbors((0, 0));

        // List should have 3 elements
        Assert.Equal(3, neighbors.Count);
        // List should contain (0, 1), (1, 0), and (1, 1) in any order
        Assert.Contains(new Position(0, 1), neighbors);
        Assert.Contains(new Position(1, 0), neighbors);
        Assert.Contains(new Position(1, 1), neighbors);
    }

    [Fact]
    public void TestTopRight()
    {
        Board board5x10 = new(5, 10);

        // Find the neighbors of the top right position
        List<Position> neighbors = board5x10.FindNeighbors((0, 9));

        // List should have 3 elements
        Assert.Equal(3, neighbors.Count);
        // List should contain (0, 8), (1, 9), and (1, 8) in any order
        Assert.Contains(new Position(0, 8), neighbors);
        Assert.Contains(new Position(1, 9), neighbors);
        Assert.Contains(new Position(1, 8), neighbors);

        // Test on a board of different dimensions
        Board board5x5 = new(5, 5);

        // Find the neighbors of the top right position
        neighbors = board5x5.FindNeighbors((0, 4));

        // List should have 3 elements
        Assert.Equal(3, neighbors.Count);
        // List should contain (0, 3), (1, 4), and (1, 3) in any order
        Assert.Contains(new Position(0, 3), neighbors);
        Assert.Contains(new Position(1, 4), neighbors);
        Assert.Contains(new Position(1, 3), neighbors);
    }

    [Fact]
    public void TestBottomRight()
    {
        Board board5x10 = new(5, 10);

        // Find the neighbors of the bottom right position
        List<Position> neighbors = board5x10.FindNeighbors((4, 9));

        // List should have 3 elements
        Assert.Equal(3, neighbors.Count);
        // List should contain (4, 8), (3, 9), and (3, 8) in any order
        Assert.Contains(new Position(4, 8), neighbors);
        Assert.Contains(new Position(3, 9), neighbors);
        Assert.Contains(new Position(3, 8), neighbors);

        // Test on a board of different dimensions
        Board board5x5 = new(5, 5);

        // Find the neighbors of the bottom right position
        neighbors = board5x5.FindNeighbors((4, 4));

        // List should have 3 elements
        Assert.Equal(3, neighbors.Count);
        // List should contain (4, 3), (3, 4), and (3, 3) in any order
        Assert.Contains(new Position(4, 3), neighbors);
        Assert.Contains(new Position(3, 4), neighbors);
        Assert.Contains(new Position(3, 3), neighbors);
    }

    [Fact]
    public void TestBottomLeft()
    {
        Board board5x10 = new(5, 10);

        // Find the neighbors of the bottom left position
        List<Position> neighbors = board5x10.FindNeighbors((4, 0));

        // List should have 3 elements
        Assert.Equal(3, neighbors.Count);
        // List should contain (4, 1), (3, 0), and (3, 1) in any order
        Assert.Contains(new Position(4, 1), neighbors);
        Assert.Contains(new Position(3, 0), neighbors);
        Assert.Contains(new Position(3, 1), neighbors);

        // Test on a board of different dimensions
        Board board5x5 = new(10, 5);

        // Find the neighbors of the bottom left position
        neighbors = board5x5.FindNeighbors((9, 0));

        // List should have 3 elements
        Assert.Equal(3, neighbors.Count);
        // List should contain (9, 1), (8, 0), and (8, 1) in any order
        Assert.Contains(new Position(9, 1), neighbors);
        Assert.Contains(new Position(8, 0), neighbors);
        Assert.Contains(new Position(8, 1), neighbors);
    }

    [Fact]
    public void TestLeftEdge()
    {
        Board board5x10 = new(5, 10);

        // Find the neighbors of a position on the left edge
        List<Position> neighbors = board5x10.FindNeighbors((2, 0));

        // List should have 3 elements
        Assert.Equal(5, neighbors.Count);
        // List should contain (1, 0), (1, 1), (2, 1), (3, 1), and (3, 0) in any order
        Assert.Contains(new Position(1, 0), neighbors);
        Assert.Contains(new Position(1, 1), neighbors);
        Assert.Contains(new Position(2, 1), neighbors);
        Assert.Contains(new Position(3, 1), neighbors);
        Assert.Contains(new Position(3, 0), neighbors);
    }

    [Fact]
    public void TestRightEdge()
    {
        Board board5x10 = new(5, 10);

        // Find the neighbors of a position on the right edge
        List<Position> neighbors = board5x10.FindNeighbors((2, 9));

        // List should have 3 elements
        Assert.Equal(5, neighbors.Count);
        // List should contain (1, 9), (1, 8), (2, 8), (3, 8), and (3, 9) in any order
        Assert.Contains(new Position(1, 9), neighbors);
        Assert.Contains(new Position(1, 8), neighbors);
        Assert.Contains(new Position(2, 8), neighbors);
        Assert.Contains(new Position(3, 8), neighbors);
        Assert.Contains(new Position(3, 9), neighbors);
    }

    [Fact]
    public void TestTopEdge()
    {
        Board board5x10 = new(5, 10);

        // Find the neighbors of a position on the top edge
        List<Position> neighbors = board5x10.FindNeighbors((0, 2));

        // List should have 3 elements
        Assert.Equal(5, neighbors.Count);
        // List should contain (0, 1), (1, 1), (1, 2), (1, 3), and (0, 3) in any order
        Assert.Contains(new Position(0, 1), neighbors);
        Assert.Contains(new Position(1, 1), neighbors);
        Assert.Contains(new Position(1, 2), neighbors);
        Assert.Contains(new Position(1, 3), neighbors);
        Assert.Contains(new Position(0, 3), neighbors);
    }

    [Fact]
    public void TestBottomEdge()
    {
        Board board5x10 = new(5, 10);

        // Find the neighbors of a position on the top edge
        List<Position> neighbors = board5x10.FindNeighbors((4, 2));

        // List should have 3 elements
        Assert.Equal(5, neighbors.Count);
        // List should contain (4, 1), (3, 1), (3, 2), (3, 3), and (4, 3) in any order
        Assert.Contains(new Position(4, 1), neighbors);
        Assert.Contains(new Position(3, 1), neighbors);
        Assert.Contains(new Position(3, 2), neighbors);
        Assert.Contains(new Position(3, 3), neighbors);
        Assert.Contains(new Position(4, 3), neighbors);
    }

    [Fact]
    public void TestCenterCell()
    {
        Board board5x10 = new(5, 10);

        // Find the neighbors of a position in the center of the board
        List<Position> neighbors = board5x10.FindNeighbors((3, 3));

        // List should have 3 elements
        Assert.Equal(8, neighbors.Count);
        // List should contain: 
        // (2, 2), (2, 3), (2, 4), (3, 4), (4, 4), (4, 3), (4, 2), (3, 2)
        Assert.Contains(new Position(2, 2), neighbors);
        Assert.Contains(new Position(2, 3), neighbors);
        Assert.Contains(new Position(2, 4), neighbors);
        Assert.Contains(new Position(3, 4), neighbors);
        Assert.Contains(new Position(4, 4), neighbors);
        Assert.Contains(new Position(4, 3), neighbors);
        Assert.Contains(new Position(4, 2), neighbors);
        Assert.Contains(new Position(3, 2), neighbors);
    }
}