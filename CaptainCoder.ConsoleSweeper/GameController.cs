using System.Text;
using CaptainCoder.Core;
using Spectre.Console;
namespace CaptainCoder.MineSweeper;

public class GameController
{
    private Position _cursorPosition = (0, 0);
    private bool _isFirstMove;
    private int _mines;
    public GameController(Board board, int mines)
    {
        Board = board;
        _isFirstMove = true;
        _mines = mines;
    }

    public Board Board { get; private set; }
    public Position CursorPosition 
    { 
        get => _cursorPosition; 
        set
        {
            _cursorPosition = value;
            _cursorPosition = Position.Min(_cursorPosition, (Board.Rows - 1, Board.Columns - 1));
            _cursorPosition = Position.Max(_cursorPosition, (0, 0));
        } 
    }

    public void Reveal()
    {
        if (_isFirstMove)
        {
            Board.PlaceRandomMines(CursorPosition, _mines);
            _isFirstMove = false;
        }
        Board.Reveal(CursorPosition);
    }

    public void ToggleFlag() => Board.ToggleFlag(CursorPosition);

    public void Render()
    {
        Console.Clear();
        // Create a table
        Panel boardPanel = new Panel(BoardContents());
        boardPanel.Header = new PanelHeader("Console Sweeper");
        AnsiConsole.Write(boardPanel);
    }

    public string BoardContents()
    {
        StringBuilder builder = new ();
        builder.Append("\n");
        for (int row = 0; row < Board.Rows; row++)
        {
            for (int col = 0; col < Board.Columns; col++)
            {
                Position position = (row, col);
                builder.Append(StylePosition(position));
            }
            builder.Append("\n");
        }
        return builder.ToString();
    }

    public char GetSymbol(Cell cell, int neighborMines)
    {
        return (cell.State, cell.Contents) switch
        {
            (CellState.Unknown, _) => '.',
            (CellState.Flagged, _) => '!',
            (CellState.Revealed, CellContents.Mine) => '*',
            (CellState.Revealed, CellContents.Empty) => neighborMines.ToString()[0],
            _ => throw new NotImplementedException()
        };
    }

    public (string, string) GetStyle(char symbol)
    {
        return symbol switch
        {
            '0' => ("[green3]", "[/]"),
            '1' => ("[springgreen3]", "[/]"),
            '2' => ("[darkcyan]", "[/]"),
            '3' => ("[purple_1]", "[/]"),
            '4' => ("[darkviolet]", "[/]"),
            '5' => ("[darkmagenta_1]", "[/]"),
            '6' => ("[mediumvioletred	]", "[/]"),
            '7' => ("[deeppink4_2]", "[/]"),
            '8' => ("[darkorange3]", "[/]"),
            '!' => ("[yellow]", "[/]"),
            '*' => ("[red]", "[/]"),
            _ => ("", ""),
        };
    }

    public string StylePosition(Position position)
    {
        Cell cell = Board.Examine(position);
        char symbol = GetSymbol(cell, Board.CountNeighborMines(position));
        (string startStyle, string endStyle) = GetStyle(symbol);
        if (position == CursorPosition)
        {
            startStyle = "[black on white]";
            endStyle = "[/]";
        }
        
        return $" {startStyle}{symbol}{endStyle} ";
    }

}