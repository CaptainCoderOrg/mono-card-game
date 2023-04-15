using CaptainCoder.Core;
namespace CaptainCoder.MineSweeper;

/// <summary>
/// A data representation of a Mine Sweeper board.
/// </summary>
public class Board
{
    private Cell[,] _grid;

    /// <summary>
    /// Instantiates an empty <see cref="Board"/> with the specified dimensions
    /// and initializes all cells to be <see cref="CellState.Unknown"/>, <see
    /// cref="CellContents.Empty"/>, and having 0 neighbor mines.
    /// </summary>
    /// <param name="rows"></param>
    /// <param name="columns"></param>
    public Board(int rows, int columns)
    {
        if (rows <= 0 || columns <= 0)
        {
            throw new ArgumentException($"Cannot construct {nameof(Board)} with 0 or fewer rows / columns.");
        }
        (Rows, Columns) = (rows, columns);
        State = BoardState.Playing;
        _grid = InitEmptyGrid(Rows, Columns);
    }
    /// <summary>
    /// The number of rows on this <see cref="Board"/>.
    /// </summary>
    /// <value></value>
    public int Rows { get; }
    /// <summary>
    /// The number of columns on this <see cref="Board"/>.
    /// </summary>
    /// <value></value>
    public int Columns { get; }
    /// <summary>
    /// The number of <see cref="Cell"/>s that contain a Mines on this <see cref="Board"/>.
    /// </summary>
    public int Mines
    {
        get
        {
            int mines = 0;
            foreach (Position pos in Positions)
            {
                Cell c = Examine(pos);
                if (c.Contents == CellContents.Mine) { mines++; }
            }
            return mines;
        }
    }
    /// <summary>
    /// The number of <see cref="Cell"/>s that have been revealed on this <see cref="Board"/>.
    /// </summary>
    public int Revealed
    {
        get
        {
            int revealed = 0;
            foreach (Position pos in Positions)
            {
                Cell c = Examine(pos);
                if (c.State == CellState.Revealed) { revealed++; }
            }
            return revealed;
        }
    }
    /// <summary>
    /// The current <see cref="BoardState"/> of this <see cref="State"/>.
    /// </summary>
    public BoardState State { get; private set; }

    /// <summary>
    /// Returns the <see cref="Cell"/> at the specified <paramref name="position"/>
    /// </summary>
    public Cell Examine(Position position) => _grid[position.Row, position.Col];

    /// <summary>
    /// Attempts to reveal the specified <paramref name="position"/>. This method uses the following rules:<br/>
    /// <li>If the BoardState is not <see cref="BoardState.Playing"/>, this method simply returns the board State.</li><br/>
    /// <li>If the cell at the specified <paramref name="position"/> is already <see cref="CellState.Unknown"/> or is <see cref="CellState.Flagged"/>, this method simply returns the board State</li><br/>
    /// <li>If the cell at the specified <paramref name="position"/> is a bomb, the State is set to <see cref="BoardState.BlownUp"/> and all cells are revealed.</li><br/>
    /// <li>Otherwise, the cell is revealed. If the revealed cell has no neighboring mines, the cells are revealed in a "flood-fill" fashion.</li><br/>
    /// <li>If all of the non-mine cells have been revealed, the State is set to <see cref="BoardState.Win"/> and all mine cells are revealed.</li><br/>
    /// </summary>
    public BoardState Reveal(Position position)
    {
        if (State == BoardState.BlownUp) { return BoardState.BlownUp; }
        if (State == BoardState.Win) { return BoardState.Win; }
        Cell cell = Examine(position);
        if (cell.State == CellState.Revealed || cell.State == CellState.Flagged) { return State; }
        if (cell.Contents == CellContents.Mine)
        {
            State = BoardState.BlownUp;
            RevealAll();
            return State;
        }

        FloodReveal(position);
        // TODO: Double check math on this one
        if (Revealed == (Rows * Columns) - Mines)
        {
            State = BoardState.Win;
            RevealAll();
        }
        return State;
    }

    /// <summary>
    /// Reveals the specified <paramref name="position"/>. If the position was
    /// previously revealed, this method does nothing. Otherwise, if the
    /// revealed cell has no neighboring mines, reveals all neighbor cells.
    /// </summary>
    public void FloodReveal(Position position)
    {
        Cell cell = Examine(position);
        if (cell.State == CellState.Revealed) { return; }
        _grid[position.Row, position.Col] = cell with { State = CellState.Revealed };
        if (cell.NeighborMines == 0)
        {
            foreach (Position neighbor in Neighbors(position))
            {
                FloodReveal(neighbor);
            }
        }
    }

    /// <summary>
    /// Marks all Cells on this <see cref="Board"/> as <see cref="CellState.Revealed"/>.
    /// </summary>
    public void RevealAll()
    {
        foreach (Position position in Positions)
        {
            Cell cell = Examine(position);
            _grid[position.Row, position.Col] = cell with { State = CellState.Revealed };
        }
    }

    /// <summary>
    /// Given a <paramref name="position"/> toggles its <see cref="CellState"/>
    /// between <see cref="CellState.Unknown"/> and <see
    /// cref="CellState.Flagged"/>. Returns the resulting <see
    /// cref="CellState"/>. If the state was Revealed, the state is unchanged.
    /// </summary>
    public CellState ToggleFlag(Position position)
    {
        Cell cell = Examine(position);
        CellState nextState = cell.State switch
        {
            CellState.Revealed => CellState.Revealed,
            CellState.Unknown => CellState.Flagged,
            CellState.Flagged => CellState.Unknown,
            _ => throw new NotImplementedException(),
        };
        _grid[position.Row, position.Col] = cell with { State = nextState };
        return nextState;
    }

    /// <summary>
    /// Given a <paramref name="position"/> return all valid neighbor positions.
    /// A position is a valid neighbor position if it is within the boards
    /// bounds and not the specified position.
    /// </summary>
    public List<Position> Neighbors(Position position)
    {
        List<Position> neighbors = new();
        for (int row = -1; row <= 1; row++)
        {
            for (int col = -1; col <= 1; col++)
            {
                Position neighborPosition = position + (row, col);
                if (neighborPosition == position) { continue; }
                if (neighborPosition.Row < 0 || neighborPosition.Row >= Rows ||
                    neighborPosition.Col < 0 || neighborPosition.Col >= Columns) { continue; }
                neighbors.Add(position);
            }
        }
        return neighbors;
    }

    /// <summary>
    /// Provides an enumerable of all positions on this <see cref="SweeperBoard"/>
    /// </summary>
    public List<Position> Positions
    {
        get
        {
            List<Position> positions = new();
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    positions.Add((row, col));
                }
            }
            return positions;
        }
    }

    /// <summary>
    /// Given a board position, counts all of the neighboring mines
    /// </summary>
    public int CountNeighborMines(Position position)
    {
        int neighborMines = 0;
        foreach (Position neighbor in Neighbors(position))
        {
            Cell cell = _grid[neighbor.Row, neighbor.Col];
            if (cell.Contents == CellContents.Mine)
            {
                neighborMines++;
            }
        }
        return neighborMines;
    }

    /// <summary>
    /// Places mines on the grid in the specified <paramref name="positions"/>.t
    /// </summary>
    public void PlaceMines(List<Position> positions)
    {
        foreach (Position position in positions)
        {
            Cell cell = Examine(position);
            _grid[position.Row, position.Col] = cell with { Contents = CellContents.Mine };
        }
        InitMineCounts();
    }

    /// <summary>
    /// Randomly places the specified number of mines ensuring that the
    /// specified <paramref name="emptyPosition"/> does not have a mine.
    /// </summary>
    public void PlaceRandomMines(Position emptyPosition, int mineCount)
    {
        List<Position> minePlacements = Positions.Shuffle().Where(p => p != emptyPosition).Take(mineCount).ToList();
        PlaceMines(minePlacements);
    }

    /// <summary>
    /// Initializes the neighbor mine counts.
    /// </summary>
    public void InitMineCounts()
    {
        foreach (Position position in Positions)
        {
            int neighborMines = CountNeighborMines(position);
            Cell cell = Examine(position);
            _grid[position.Row, position.Col] = cell with { NeighborMines = neighborMines };
        }
    }

    /// <summary>
    /// Creates a 2D array of Cells with the specified number of <paramref
    /// name="rows"/> and <paramref name="columns"/>. Each cell is initialized
    /// to be cell with <see cref="CellState.Unknown"/>, <see
    /// cref="CellContents.Empty"/, and 0 neighbor mines.
    /// </summary>
    public static Cell[,] InitEmptyGrid(int rows, int columns)
    {
        Cell[,] grid = new Cell[rows, columns];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                grid[row, col] = new Cell(CellState.Unknown, CellContents.Empty, 0);
            }
        }
        return grid;
    }
}