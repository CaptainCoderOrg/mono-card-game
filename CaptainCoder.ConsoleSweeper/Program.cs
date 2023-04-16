using Spectre.Console;
using CaptainCoder.Core;
using CaptainCoder.MineSweeper;

// See https://aka.ms/new-console-template for more information

const int ROWS = 10;
const int COLUMNS = 10;
const int MINES = 10;

Board board = new (ROWS, COLUMNS);
GameController controller = new (board, MINES);

do
{
    controller.Render();
    ConsoleKeyInfo keyinfo = Console.ReadKey();
    HandleInput(keyinfo);
} while (board.State == BoardState.Playing);

controller.Render();

if (board.State == BoardState.Win)
{
    AnsiConsole.Markup("You Win!");
}
else if (board.State == BoardState.BlownUp) 
{
    AnsiConsole.Markup("You blowed up!");
}
else
{
    AnsiConsole.Markup("Something unexpected happened.");
}

void HandleInput(ConsoleKeyInfo info)
{
    UpdatePosition(info);
    CheckReveal(info);
}

void CheckReveal(ConsoleKeyInfo info)
{
    if (info.Key == ConsoleKey.Spacebar)
    {
        controller.Reveal();
    }
    else if (info.Key == ConsoleKey.F)
    {
        controller.ToggleFlag();
    }
} 

void UpdatePosition(ConsoleKeyInfo info)
{
    Position positionUpdate = info.Key switch
    {
        ConsoleKey.LeftArrow => (0, -1),
        ConsoleKey.RightArrow => (0, 1),
        ConsoleKey.UpArrow => (-1, 0),
        ConsoleKey.DownArrow => (1, 0),
        _ => (0, 0),
    };
    controller.CursorPosition += positionUpdate;
}
