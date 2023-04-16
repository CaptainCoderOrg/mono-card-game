using CaptainCoder.Core;
namespace CaptainCoder.MineSweeper.Tests;

public class ToggleTest
{
    [Fact]
    public void TestToggleSimpleFlag()
    {
        Board board5x10 = new(5, 10);
        Assert.Equal(0, board5x10.Revealed);

        // First time toggling a flag should turn a flag on
        CellState state = board5x10.ToggleFlag((1, 1));
        Assert.Equal(CellState.Flagged, state);

        Cell examined = board5x10.Examine((1,1));
        Cell expected = new Cell(CellState.Flagged, CellContents.Empty);
        Assert.Equal(expected, examined);

        // Toggling a second time turns the flag off
        state = board5x10.ToggleFlag((1, 1));
        Assert.Equal(CellState.Unknown, state);
        examined = board5x10.Examine((1,1));
        expected = new Cell(CellState.Unknown, CellContents.Empty);
        Assert.Equal(expected, examined);
    }

    [Fact]
    public void TestToggleOnRevealed()
    {

        Board board = new(4, 4);

        List<Position> positions = new()
        {
            (1, 1),
        };
        board.PlaceMines(positions);

        // Reveal the top left corner
        BoardState state = board.Reveal((0,0));
        // We didn't blow up so the state is still Playing
        Assert.Equal(BoardState.Playing, state);

        // There should be 1 revealed cells
        Assert.Equal(1, board.Revealed);

        // The cell should now be revealed
        Cell toExamine = board.Examine((0,0));
        Cell expected = new (CellState.Revealed, CellContents.Empty);
        Assert.Equal(expected, toExamine);

        // Attempting to toggle a revealed cell should have no change
        CellState cellState = board.ToggleFlag((0, 0));
        Assert.Equal(CellState.Revealed, cellState);

        Cell examined = board.Examine((0,0));
        expected = new Cell(CellState.Revealed, CellContents.Empty);
        Assert.Equal(expected, examined);
    }

    [Fact]
    public void TestToggleFloodReveal()
    {
        // Tests that a toggled cell, is revealed if a flood reveal would reveal that cell
        Board board = new(4, 4);

        List<Position> positions = new()
        {
            (1, 1),
        };
        board.PlaceMines(positions);

        CellState flagged = board.ToggleFlag((0,3));
        Assert.Equal(CellState.Flagged, flagged);

        // Reveal the bottom left corner
        BoardState state = board.Reveal((3,0));
        // The game continues
        Assert.Equal(BoardState.Playing, state);

        // Doing so should reveal the following state:
        //   column
        //     0123
        //
        // r 0 ..1F
        // o 1 ..10 
        // w 2 1110
        //   3 0000

        Cell cell = board.Examine((0,3));
        Cell expected = new (CellState.Revealed, CellContents.Empty);
        Assert.Equal(expected, cell);


    }
}