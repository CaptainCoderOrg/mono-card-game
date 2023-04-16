using CaptainCoder.Core;
namespace CaptainCoder.MineSweeper.Tests;

public class ConstructorTest
{
    [Fact]
    public void TestRowsAndColumns()
    {
        // Tests constructing a Board with 5 rows and 10 columns
        Board board5x10 = new(5, 10);
        // The Rows property should be 5
        Assert.Equal(5, board5x10.Rows);
        // The Columns property should be 10
        Assert.Equal(10, board5x10.Columns);

        // Tests constructing a Board with 10 rows and 12 columns
        Board board10x12 = new(10, 12);
        // The Rows property should be 10
        Assert.Equal(10, board10x12.Rows);
        // The Columns property should be 12
        Assert.Equal(12, board10x12.Columns);
    }

    [Fact]
    public void TestConstructorBoardState()
    {
        // Tests constructing a Board with 5 rows and 10 columns
        Board board5x10 = new(5, 10);
        // The game states should be initialized to Playing
        Assert.Equal(BoardState.Playing, board5x10.State);

        // Tests constructing a Board with 5 rows and 10 columns
        Board board10x12 = new(10, 12);
        // The game states should be initialized to Playing
        Assert.Equal(BoardState.Playing, board10x12.State);
    }

    [Fact]
    public void TestPositions()
    {
        // Tests constructing a Board with 5 rows and 10 columns
        Board board5x10 = new(5, 10);

        List<Position> positions = board5x10.Positions;
        // There should be 50 positions on this board
        Assert.Equal(50, positions.Count);

        // Checks that all of the positions are correct
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 10; col++)
            {
                Assert.True(positions.Contains(new Position(row, col)), $"The board did not contain {(row, col)}");
            }
        }

        // Tests constructing a Board with 5 rows and 10 columns
        Board board10x12 = new(10, 12);

        positions = board10x12.Positions;
        // There should be 50 positions on this board
        Assert.Equal(120, positions.Count);

        // Checks that all of the positions are correct
        for (int row = 0; row < 10; row++)
        {
            for (int col = 0; col < 12; col++)
            {
                Assert.True(positions.Contains(new Position(row, col)), $"The board did not contain {(row, col)}");
            }
        }
    }

    [Fact]
    public void TestInitialCellStates()
    {
        Board board5x10 = new(5, 10);

        foreach (Position position in board5x10.Positions)
        {
            Cell expected = new(CellState.Unknown, CellContents.Empty);
            Cell actual = board5x10.Examine((0, 0));
            Assert.True(expected == actual, $"The cell at {position} was initialized to {actual}");
        }
    }
    
}