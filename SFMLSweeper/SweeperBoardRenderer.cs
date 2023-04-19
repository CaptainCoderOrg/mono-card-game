using CaptainCoder.MineSweeper;
using CaptainCoder.Core;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class SweeperBoardRenderer
{
    public const int CellSize = 49;
    private readonly static Dictionary<SweeperImage, Texture> _textures = new();
    static SweeperBoardRenderer()
    {
        LoadTextures();
    }

    private Board _board;
    private Dictionary<Position, Sprite> _sprites = new();

    public SweeperBoardRenderer(Board board)
    {
        Board = board;
    }

    public Board Board
    {
        get => _board;
        set
        {
            _board = value;
            InitSprites();
        }
    }

    private bool _isFirstClick = true;

    public void HandleMouseButtonPressed(object? sender, MouseButtonEventArgs evt)
    {
        Position pos = (evt.Y / CellSize, evt.X / CellSize);
        if (!_board.Positions.Contains(pos)) { return;  }
        if (_isFirstClick)
        {
            Board.PlaceRandomMines(pos, 10);
            _isFirstClick = false;
        }
        if (evt.Button == Mouse.Button.Left)
        {
            Board.Reveal(pos);
        }
        else if (evt.Button == Mouse.Button.Right)
        {
            Board.ToggleFlag(pos);
        }

        foreach (Position position in Board.Positions)
        {
            UpdateSprite(position);
        }
    }

    public void Render(RenderWindow window)
    {
        foreach (Position position in _board.Positions)
        {
            // UpdateSprite(position);
            // Update sprite textures            
            Sprite toDraw = _sprites[position];
            window.Draw(toDraw);
        }
    }

    private void UpdateSprite(Position position)
    {
        Cell examined = _board.Examine(position);
        SweeperImage image = examined.State switch
        {
            CellState.Unknown => SweeperImage.Blank,
            CellState.Flagged => SweeperImage.Flag,
            CellState.Revealed => RevealedImage(examined.Contents, _board.CountNeighborMines(position)),
            _ => throw new NotImplementedException()
        };
        Sprite toUpdate = _sprites[position];
        toUpdate.Texture = _textures[image];
    }

    private static SweeperImage RevealedImage(CellContents contents, int neighborMines)
    {
        if (contents == CellContents.Mine) { return SweeperImage.Mine; }
        return neighborMines switch
        {
            // TODO: Need a cleared image
            0 => SweeperImage.Cleared,
            1 => SweeperImage.One,
            2 => SweeperImage.Two,
            3 => SweeperImage.Three,
            4 => SweeperImage.Four,
            5 => SweeperImage.Five,
            6 => SweeperImage.Six,
            7 => SweeperImage.Seven,
            8 => SweeperImage.Eight,
            _ => throw new NotImplementedException(),
        };
    }

    private void InitSprites()
    {
        foreach (Position position in _board.Positions)
        {
            Sprite cellSprite = new(_textures[SweeperImage.Blank])
            {
                Position = new(position.Col * CellSize, position.Row * CellSize)
            };
            _sprites[position] = cellSprite;
        }
    }


    public static void LoadTextures()
    {
        if (_textures.Count > 0) { return; }
        _textures[SweeperImage.Cleared] = new("Assets/buttons/cleared.png");
        _textures[SweeperImage.One] = new("Assets/buttons/1.png");
        _textures[SweeperImage.Two] = new("Assets/buttons/2.png");
        _textures[SweeperImage.Three] = new("Assets/buttons/3.png");
        _textures[SweeperImage.Four] = new("Assets/buttons/4.png");
        _textures[SweeperImage.Five] = new("Assets/buttons/5.png");
        _textures[SweeperImage.Six] = new("Assets/buttons/6.png");
        _textures[SweeperImage.Seven] = new("Assets/buttons/7.png");
        _textures[SweeperImage.Eight] = new("Assets/buttons/8.png");
        _textures[SweeperImage.Blank] = new("Assets/buttons/blank.png");
        _textures[SweeperImage.Flag] = new("Assets/buttons/flag.png");
        _textures[SweeperImage.Mine] = new("Assets/buttons/mine.png");
    }

}

public enum SweeperImage
{
    Cleared,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Blank,
    Flag,
    Mine
}