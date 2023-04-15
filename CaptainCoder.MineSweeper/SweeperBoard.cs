using CaptainCoder.Core;
namespace CaptainCoder.MineSweeper;
public class Board
{
    private Cell[,] _grid;
    private bool _isGenerated;

    public Board(int rows, int columns, int mines)
    {
        if (rows <= 0 || columns <= 0)
        {
            throw new ArgumentException($"Cannot construct {nameof(Board)} with 0 or fewer rows / columns.");
        }
        if (mines <= 0)
        {
            throw new ArgumentException($"Cannot construct {nameof(Board)} with 0 or fewer mines.");
        }
        if (rows * columns < mines)
        {
            throw new ArgumentException($"Cannot construct {nameof(Board)} with more mines than spaces.");
        }
        (Rows, Columns, Mines) = (rows, columns, mines);
        State = BoardState.Playing;
        _grid = InitGrid();
        _isGenerated = false;
    }
    public int Rows { get; }
    public int Columns { get; }
    public int Mines { get; }
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
    public BoardState State { get; private set; }

    public Cell Examine(Position position) => _grid[position.Row, position.Col];
    public BoardState Reveal(Position position)
    {
        if (State == BoardState.BlownUp) { return BoardState.BlownUp; }
        if (State == BoardState.Win) { return BoardState.Win; }
        if (!_isGenerated) { GenerateGrid(position); }
        Cell cell = Examine(position);
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

    public void FloodReveal(Position position)
    {
        Cell cell = Examine(position);
        if (cell.State == CellState.Revealed) { return; }
        _grid[position.Row, position.Col] = cell with { State = CellState.Revealed };
        if (cell.NeighborMines == 0)
        {
            foreach(Position neighbor in Neighbors(position))
            {
                FloodReveal(neighbor);
            }
        }
    }

    public void RevealAll()
    {
        foreach(Position position in Positions)
        {
            Cell cell = Examine(position);
            _grid[position.Row, position.Col] = cell with { State = CellState.Revealed };
        }
    }

    public CellState FlagPosition(Position position)
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

    private void GenerateGrid(Position emptyPosition)
    {
        Random rng = new();
        HashSet<Position> bombs = new() { emptyPosition };
        while (bombs.Count <= Mines)
        {
            int row = rng.Next(0, Rows);
            int col = rng.Next(0, Columns);
            Position p = (row, col);
            if (bombs.Contains(p)) { continue; }
            bombs.Add(p);
            _grid[row, col] = new Cell(CellState.Unknown, CellContents.Mine, 0);
        }
        bombs.Remove(emptyPosition);
        InitMineCount();
        _isGenerated = true;
    }

    public IEnumerable<Position> Neighbors(Position position)
    {
        for (int row = -1; row <= 1; row++)
        {
            for (int col = -1; col <= 1; col++)
            {
                Position neighborPosition = position + (row, col);
                if (neighborPosition == position) { continue; }
                if (neighborPosition.Row < 0 || neighborPosition.Row >= Rows ||
                    neighborPosition.Col < 0 || neighborPosition.Col >= Columns) { continue; }
                yield return neighborPosition;
            }
        }
    }

    public IEnumerable<Position> Positions
    {
        get
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    yield return new Position(row, col);
                }
            }
        }
    }

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

    private void InitMineCount()
    {
        foreach (Position position in Positions)
        {
            int neighborMines = CountNeighborMines(position);
            Cell cell = Examine(position);
            _grid[position.Row, position.Col] = cell with { NeighborMines = neighborMines };
        }
    }

    private Cell[,] InitGrid()
    {
        Cell[,] grid = new Cell[Rows, Columns];
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                _grid[row, col] = new Cell(CellState.Unknown, CellContents.Empty, 0);
            }
        }
        return grid;
    }
}