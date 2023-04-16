using CaptainCoder.Core;
namespace CaptainCoder.MineSweeper.Tests;

public class PlaceMinesTest
{

    [Fact]
    public void TestMinesCountZero()
    {
        // Tests that the number of mines counted is zero after
        // the board has been constructed.
        Board board5x10 = new(5, 10);
        Assert.Equal(0, board5x10.Mines);
    }

    [Fact]
    public void TestPlaceMinesSetsContent()
    {
        // Tests that placing a list of mines, sets the Contents of
        // the specified cells to be Mine
        Board board5x10 = new(5, 10);
        
        List<Position> positions = new ()
        {
            (0, 0),
            (0, 9),
            (1, 3),
            (2, 3),
            (3, 2),
            (3, 3),            
            (3, 7),
            (3, 8),
            (4, 4),
            (4, 9),
        };
        board5x10.PlaceMines(positions);

        // Checks that each position set contains a mine
        foreach (Position position in positions)
        {
            Cell actual = board5x10.Examine(position);
            Assert.True(actual.Contents == CellContents.Mine, $"The cell at {position} had the state {actual.Contents}");
        }
    }

    [Fact]
    public void TestPlaceMinesUpdatesMinesCount()
    {
        // Tests that placing a list of mines, sets the Contents of
        // the specified cells to be Mine
        Board board5x10 = new(5, 10);
        
        List<Position> positions = new ()
        {
            (0, 0),
            (0, 9),
            (1, 3),
            (2, 3),
            (3, 2),
            (3, 3),            
            (3, 7),
            (3, 8),
            (4, 4),
            (4, 9),
        };
        board5x10.PlaceMines(positions);

        // Checks that the number of mines on the board is 10
        Assert.Equal(10, board5x10.Mines);
    }

}