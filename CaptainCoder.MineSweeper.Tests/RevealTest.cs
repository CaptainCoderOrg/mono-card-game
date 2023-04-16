using CaptainCoder.Core;
namespace CaptainCoder.MineSweeper.Tests;

public class RevealTest
{
    [Fact]
    public void TestConstructRevealIsZero()
    {
        // When a board is constructed, it should have 0
        // cells revealed
        Board board5x10 = new(5, 10);
        Assert.Equal(0, board5x10.Revealed);
    }

    [Fact]
    public void TestRevealSingle()
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

        // No other cells should be revealed
        foreach (Position position in board.Positions)
        {
            // Skip the top left position
            if (position == new Position(0,0)) { continue; } 

            Cell toCheck = board.Examine(position);
            Assert.True(toCheck.State == CellState.Unknown, $"The cell at {position} was {toCheck.State}");
        }
    }

    [Fact]
    public void TestRevealBomb()
    {
        Board board = new(4, 4);

        List<Position> positions = new()
        {
            (1, 1),
        };
        board.PlaceMines(positions);

        // Reveal the top left corner
        BoardState state = board.Reveal((1,1));
        // We blew up
        Assert.Equal(BoardState.BlownUp, state);

        // All cells should be revealed
        foreach (Position position in board.Positions)
        {

            Cell toCheck = board.Examine(position);
            Assert.True(toCheck.State == CellState.Revealed, $"The cell at {position} was {toCheck.State}");
        }

        // Any further move should not change the state of the board

        foreach (Position position in board.Positions)
        {

            state = board.Reveal(position);
            Assert.True(state == BoardState.BlownUp, $"The board state changed when revealing {position}");
        }
    }

    [Fact]
    public void TestFloodRevealBottomRight()
    {
        Board board = new(4, 4);

        List<Position> positions = new()
        {
            (1, 1),
        };
        board.PlaceMines(positions);

        // Reveal the bottom left corner
        BoardState state = board.Reveal((0,3));
        // The game continues
        Assert.Equal(BoardState.Playing, state);

        // Doing so should reveal the following state:
        //   column
        //     0123
        //
        // r 0 ??10 
        // o 1 ??10 
        // w 2 1110
        //   3 0000

        List<Position> revealedPositions = new ()
        {
                            (0, 2), (0, 3),
                            (1, 2), (1, 3),
            (2, 0), (2, 1), (2, 2), (2, 3),
            (3, 0), (3, 1), (3, 2), (3, 3),
        };

        // There should be 12 revealed cells
        Assert.Equal(12, board.Revealed);
        
        foreach (Position position in revealedPositions)
        {
            Cell toCheck = board.Examine(position);
            Assert.True(toCheck.State == CellState.Revealed, $"The cell at {position} was {toCheck.State}");
        }

        List<Position> unrevealedPositions = new ()
        {
            (0, 0), (0, 1),
            (1, 0), (1, 1),
        };

        foreach (Position position in unrevealedPositions)
        {
            Cell toCheck = board.Examine(position);
            Assert.True(toCheck.State == CellState.Unknown, $"The cell at {position} was {toCheck.State}");
        }
    }

    [Fact]
    public void TestFloodRevealTopLeft()
    {
        Board board = new(4, 4);

        List<Position> positions = new()
        {
            (2, 2),
        };
        board.PlaceMines(positions);

        // Reveal the bottom left corner
        BoardState state = board.Reveal((0,0));
        // The game continues
        Assert.Equal(BoardState.Playing, state);

        // Doing so should reveal the following state:
        //   column
        //     0123
        //
        // r 0 0000 
        // o 1 0111 
        // w 2 01??
        //   3 01??

        List<Position> revealedPositions = new ()
        {
            (0, 0), (0, 1), (0, 2), (0, 3),
            (1, 0), (1, 1), (1, 2), (1, 3),
            (2, 0), (2, 1), 
            (3, 0), (3, 1), 
        };

        // There should be 12 revealed cells
        Assert.Equal(12, board.Revealed);
        
        foreach (Position position in revealedPositions)
        {
            Cell toCheck = board.Examine(position);
            Assert.True(toCheck.State == CellState.Revealed, $"The cell at {position} was {toCheck.State}");
        }

        List<Position> unrevealedPositions = new ()
        {
            (2, 2), (2, 3),
            (3, 2), (3, 3),
        };

        foreach (Position position in unrevealedPositions)
        {
            Cell toCheck = board.Examine(position);
            Assert.True(toCheck.State == CellState.Unknown, $"The cell at {position} was {toCheck.State}");
        }
    }

    [Fact]
    public void TestFloodRevealCenter()
    {
        Board board = new(4, 5);

        List<Position> positions = new()
        {
            (0, 0),        (0, 2)
                  , (1, 1)
        };
        board.PlaceMines(positions);

        // Reveal the bottom left corner
        BoardState state = board.Reveal((2,3));
        // The game continues
        Assert.Equal(BoardState.Playing, state);

        // Doing so should reveal the following state:
        //   column
        //     01234
        //
        // r 0 ???10 
        // o 1 ??210 
        // w 2 11100
        //   3 00000

        List<Position> revealedPositions = new ()
        {
                                    (0, 3), (0, 4),
                            (1, 2), (1, 3), (1, 4),
            (2, 0), (2, 1), (2, 2), (2, 3), (2, 4),
            (3, 0), (3, 1), (3, 2), (3, 3), (3, 4),
        };

        // There should be 15 revealed cells
        Assert.Equal(15, board.Revealed);
        
        foreach (Position position in revealedPositions)
        {
            Cell toCheck = board.Examine(position);
            Assert.True(toCheck.State == CellState.Revealed, $"The cell at {position} was {toCheck.State}");
        }

        List<Position> unrevealedPositions = new ()
        {
            (0, 0), (0, 1), (0, 2)
                  , (1, 1)
        };

        foreach (Position position in unrevealedPositions)
        {
            Cell toCheck = board.Examine(position);
            Assert.True(toCheck.State == CellState.Unknown, $"The cell at {position} was {toCheck.State}");
        }
    }

    [Fact]
    public void TestFloodRevealAndWin()
    {
        Board board = new(4, 5);

        List<Position> positions = new()
        {
            (0, 0)
        };
        board.PlaceMines(positions);

        // Reveal the bottom left corner
        BoardState state = board.Reveal((2,3));
        // The game should be won
        Assert.Equal(BoardState.Win, state);
        
        // All positions should be revealed
        foreach (Position position in board.Positions)
        {
            Cell toCheck = board.Examine(position);
            Assert.True(toCheck.State == CellState.Revealed, $"The cell at {position} was {toCheck.State}");
        }

        // Revealing additional locations should not change the state
        foreach (Position position in board.Positions)
        {

            state = board.Reveal(position);
            Assert.True(state == BoardState.Win, $"The board state changed when revealing {position}");
        }
    }

}