using System.Text;
using CaptainCoder.Core;
using Spectre.Console;
namespace CaptainCoder.MineSweeper;

public class GameController
{
    private Position _cursorPosition = (0, 0);
    private bool _isFirstMove;
    private int _mines;
    public GameController(int rows, int cols, int mines)
    {
        NewGame(rows, cols, mines);
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
        
        Panel boardPanel = new (BoardContents());

        Panel controlsPanel = new (Info);
        controlsPanel.Header = new PanelHeader("Controls");

        var rightColumn = new Rows(
                    controlsPanel,
                    GetGameInfo()
                );
        var mainGame = new Columns(
                boardPanel, 
                rightColumn
        ).Collapse();

        string TitleText = Board.State switch
        {
            BoardState.BlownUp => "BOOM",
            BoardState.Win => "You Win",
            BoardState.Playing => "Sweeper",
            _ => throw new NotImplementedException()
        };     

        var layout = new Rows(
            new FigletText(TitleText).Color(Spectre.Console.Color.Red).Centered(),
            new Text(""),
            Align.Center(mainGame)
        );

        layout.Collapse();
        
        AnsiConsole.Write(layout);
    }

    public void NewGame(int rows, int cols, int mines)
    {
        Board = new Board(rows, cols);
        _isFirstMove = true;
        CursorPosition = (0, 0);
        _mines = mines;
    }

    public Panel GetGameInfo()
    {
        string info = 
        $"""
        Mines: {_mines}
        Flagged: {Board.Flags}
        Status: {Board.State}
        Revealed: {Board.Revealed}
        Remaining: {Board.Rows * Board.Columns - Board.Revealed}
        """;
        Panel gameInfo = new (info);
        gameInfo.Header = new PanelHeader("Game Info");
        gameInfo.Width = 60;
        return gameInfo;
    }

    public string Info { get; } = 
    """
    Arrow Keys - Move Cursor
    F - Flag a Space
    Space Bar - Reveal Space
    1 - New Game Easy
    2 - New Game Medium
    3 - New Game Hard
    ESC - Exit
    """.Trim();

    public string BoardContents()
    {
        StringBuilder builder = new ();
        for (int row = 0; row < Board.Rows; row++)
        {
            if (row > 0) { builder.Append("\n"); }
            for (int col = 0; col < Board.Columns; col++)
            {
                Position position = (row, col);
                builder.Append(StylePosition(position));
            }
        }
        return builder.ToString();
    }

    public char GetSymbol(Cell cell, int neighborMines)
    {
        return (cell.State, cell.Contents, neighborMines) switch
        {
            (CellState.Unknown, _, _) => '.',
            (CellState.Flagged, _, _) => '!',
            (CellState.Revealed, CellContents.Mine, _) => '*',
            (CellState.Revealed, CellContents.Empty, 0) => ' ',
            (CellState.Revealed, CellContents.Empty, _) => neighborMines.ToString()[0],
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
            '6' => ("[mediumvioletred]", "[/]"),
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