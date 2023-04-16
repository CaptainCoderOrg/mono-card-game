using CaptainCoder.Core;
namespace CaptainCoder.MineSweeper.Tests;

public class CountNeighborsTest
{

    [Fact]
    public void Test1NeighborMine()
    {
        // This test uses the following board setup:
        
        //   column
        //     0123
        //
        // r 0 1110 
        // o 1 1*10 
        // w 2 1110
        //   3 0000

        // Tests that placing a list of mines, sets the Contents of
        // the specified cells to be Mine
        Board board = new(4, 4);
        
        List<Position> positions = new ()
        {
            (1, 1),
        };
        board.PlaceMines(positions);

        Assert.Equal(1, board.CountNeighborMines((0, 0)));
        Assert.Equal(1, board.CountNeighborMines((0, 1)));
        Assert.Equal(1, board.CountNeighborMines((0, 2)));
        Assert.Equal(0, board.CountNeighborMines((0, 3)));

        Assert.Equal(1, board.CountNeighborMines((1, 0)));
        Assert.Equal(0, board.CountNeighborMines((1, 1)));
        Assert.Equal(1, board.CountNeighborMines((1, 2)));
        Assert.Equal(0, board.CountNeighborMines((1, 3)));

        Assert.Equal(1, board.CountNeighborMines((2, 0)));
        Assert.Equal(1, board.CountNeighborMines((2, 1)));
        Assert.Equal(1, board.CountNeighborMines((2, 2)));
        Assert.Equal(0, board.CountNeighborMines((2, 3)));

        Assert.Equal(0, board.CountNeighborMines((3, 0)));
        Assert.Equal(0, board.CountNeighborMines((3, 1)));
        Assert.Equal(0, board.CountNeighborMines((3, 2)));
        Assert.Equal(0, board.CountNeighborMines((3, 3)));
    }  

    [Fact]
    public void Test2NeighborMines()
    {
        // This test uses the following board setup:
        
        //   column
        //     0123
        //
        // r 0 1221 
        // o 1 2**2 
        // w 2 2**2
        //   3 1221

        // Tests that placing a list of mines, sets the Contents of
        // the specified cells to be Mine
        Board board = new(4, 4);
        
        List<Position> positions = new ()
        {
            (1, 1), (1, 2),
            (2, 1), (2, 2),
        };
        board.PlaceMines(positions);

        Assert.Equal(1, board.CountNeighborMines((0, 0)));
        Assert.Equal(2, board.CountNeighborMines((0, 1)));
        Assert.Equal(2, board.CountNeighborMines((0, 2)));
        Assert.Equal(1, board.CountNeighborMines((0, 3)));

        Assert.Equal(2, board.CountNeighborMines((1, 0)));
        Assert.Equal(2, board.CountNeighborMines((1, 3)));

        Assert.Equal(2, board.CountNeighborMines((2, 0)));
        Assert.Equal(2, board.CountNeighborMines((2, 3)));

        Assert.Equal(1, board.CountNeighborMines((3, 0)));
        Assert.Equal(2, board.CountNeighborMines((3, 1)));
        Assert.Equal(2, board.CountNeighborMines((3, 2)));
        Assert.Equal(1, board.CountNeighborMines((3, 3)));
    }

    [Fact]
    public void Test3NeighborMines()
    {
        // This test uses the following board setup:
        
        //   column
        //     0123
        //
        // r 0 1232 
        // o 1 1*** 
        // w 2 1232
        //   3 0000

        // Tests that placing a list of mines, sets the Contents of
        // the specified cells to be Mine
        Board board = new(4, 4);
        
        List<Position> positions = new ()
        {
            (1, 1), (1, 2), (1, 3)
        };
        board.PlaceMines(positions);

        Assert.Equal(1, board.CountNeighborMines((0, 0)));
        Assert.Equal(2, board.CountNeighborMines((0, 1)));
        Assert.Equal(3, board.CountNeighborMines((0, 2)));
        Assert.Equal(2, board.CountNeighborMines((0, 3)));

        Assert.Equal(1, board.CountNeighborMines((1, 0)));

        Assert.Equal(1, board.CountNeighborMines((2, 0)));
        Assert.Equal(2, board.CountNeighborMines((2, 1)));
        Assert.Equal(3, board.CountNeighborMines((2, 2)));
        Assert.Equal(2, board.CountNeighborMines((2, 3)));
    }

    [Fact]
    public void Test4NeighborMines()
    {
        // This test uses the following board setup:
        
        //   column
        //     0123
        //
        // r 0 124* 
        // o 1 1*** 
        // w 2 124*
        //   3 0000

        // Tests that placing a list of mines, sets the Contents of
        // the specified cells to be Mine
        Board board = new(4, 4);
        
        List<Position> positions = new ()
        {
                            (0, 3),
            (1, 1), (1, 2), (1, 3),
                            (2, 3),
        };
        board.PlaceMines(positions);

        Assert.Equal(1, board.CountNeighborMines((0, 0)));
        Assert.Equal(2, board.CountNeighborMines((0, 1)));
        Assert.Equal(4, board.CountNeighborMines((0, 2)));

        Assert.Equal(1, board.CountNeighborMines((1, 0)));

        Assert.Equal(1, board.CountNeighborMines((2, 0)));
        Assert.Equal(2, board.CountNeighborMines((2, 1)));
        Assert.Equal(4, board.CountNeighborMines((2, 2)));
    }

    [Fact]
    public void Test5NeighborMines()
    {
        // This test uses the following board setup:
        
        //   column
        //     0123
        //
        // r 0 2*5* 
        // o 1 3*** 
        // w 2 2*5*
        //   3 1111

        // Tests that placing a list of mines, sets the Contents of
        // the specified cells to be Mine
        Board board = new(4, 4);
        
        List<Position> positions = new ()
        {
            (0, 1),         (0, 3),
            (1, 1), (1, 2), (1, 3),
            (2, 1),         (2, 3),
        };
        board.PlaceMines(positions);

        Assert.Equal(2, board.CountNeighborMines((0, 0)));
        Assert.Equal(5, board.CountNeighborMines((0, 2)));

        Assert.Equal(3, board.CountNeighborMines((1, 0)));

        Assert.Equal(2, board.CountNeighborMines((2, 0)));
        Assert.Equal(5, board.CountNeighborMines((2, 2)));
    }

    [Fact]
    public void Test6NeighborMines()
    {
        // This test uses the following board setup:
        
        //   column
        //     0123
        //
        // r 0 2*5* 
        // o 1 3*** 
        // w 2 2*6*
        //   3 12*2

        // Tests that placing a list of mines, sets the Contents of
        // the specified cells to be Mine
        Board board = new(4, 4);
        
        List<Position> positions = new ()
        {
            (0, 1),         (0, 3),
            (1, 1), (1, 2), (1, 3),
            (2, 1),         (2, 3),
                    (3, 2),
        };
        board.PlaceMines(positions);

        Assert.Equal(2, board.CountNeighborMines((0, 0)));
        Assert.Equal(5, board.CountNeighborMines((0, 2)));

        Assert.Equal(3, board.CountNeighborMines((1, 0)));

        Assert.Equal(2, board.CountNeighborMines((2, 0)));
        Assert.Equal(6, board.CountNeighborMines((2, 2)));
    }

    [Fact]
    public void Test7NeighborMines()
    {
        // This test uses the following board setup:
        
        //   column
        //     0123
        //
        // r 0 2*5* 
        // o 1 3*** 
        // w 2 3*7*
        //   3 2**2

        // Tests that placing a list of mines, sets the Contents of
        // the specified cells to be Mine
        Board board = new(4, 4);
        
        List<Position> positions = new ()
        {
            (0, 1),         (0, 3),
            (1, 1), (1, 2), (1, 3),
            (2, 1),         (2, 3),
            (3, 1), (3, 2),
        };
        board.PlaceMines(positions);

        Assert.Equal(2, board.CountNeighborMines((0, 0)));
        Assert.Equal(5, board.CountNeighborMines((0, 2)));

        Assert.Equal(3, board.CountNeighborMines((1, 0)));

        Assert.Equal(3, board.CountNeighborMines((2, 0)));
        Assert.Equal(7, board.CountNeighborMines((2, 2)));
    }

    [Fact]
    public void Test8NeighborMines()
    {
        // This test uses the following board setup:
        
        //   column
        //     0123
        //
        // r 0 2*5* 
        // o 1 3*** 
        // w 2 3*8*
        //   3 2***

        // Tests that placing a list of mines, sets the Contents of
        // the specified cells to be Mine
        Board board = new(4, 4);
        
        List<Position> positions = new ()
        {
            (0, 1),         (0, 3),
            (1, 1), (1, 2), (1, 3),
            (2, 1),         (2, 3),
            (3, 1), (3, 2), (3, 3)
        };
        board.PlaceMines(positions);

        Assert.Equal(2, board.CountNeighborMines((0, 0)));
        Assert.Equal(5, board.CountNeighborMines((0, 2)));

        Assert.Equal(3, board.CountNeighborMines((1, 0)));

        Assert.Equal(3, board.CountNeighborMines((2, 0)));
        Assert.Equal(8, board.CountNeighborMines((2, 2)));
    }

}