using Spectre.Console;
using CaptainCoder.Core;
using CaptainCoder.MineSweeper;

// See https://aka.ms/new-console-template for more information

const int ROWS = 10;
const int COLUMNS = 10;
const int MINES = 10;

bool isPlaying = true;

GameController controller = new (ROWS, COLUMNS, MINES);

do
{
    controller.Render();
    ConsoleKeyInfo keyinfo = Console.ReadKey();
    HandleInput(keyinfo);
} while (isPlaying);

void HandleInput(ConsoleKeyInfo info)
{
    UpdatePosition(info);
    CheckReveal(info);
    CheckNewGame(info);
    CheckExit(info);
}

void CheckExit(ConsoleKeyInfo info)
{
    if (info.Key == ConsoleKey.Escape)
    {
        Console.Clear();
        AnsiConsole.Write(new FigletText("Goodbye!").Color(Color.Green));
        isPlaying = false;
    }
}

void CheckNewGame(ConsoleKeyInfo info)
{
    
    if (info.Key == ConsoleKey.D1)
    {
        controller.NewGame(10, 10, 10);
    }
    else if (info.Key == ConsoleKey.D2)
    {
        controller.NewGame(12, 12, 25);
    }
    else if (info.Key == ConsoleKey.D3)
    {
        controller.NewGame(15, 15, 50);
    }
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
