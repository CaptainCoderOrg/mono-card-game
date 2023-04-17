using System.Collections.Generic;
using CaptainCoder.Core;
using CaptainCoder.MineSweeper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class SweeperButton
{
    public const int ButtonWidth = 48;
    public const int ButtonHeight = 48;
    private static Dictionary<ButtonSprite, Texture2D> _sprites;
    public static void LoadGraphics(ContentManager content)
    {
        _sprites = new Dictionary<ButtonSprite, Texture2D>();
        _sprites[ButtonSprite.One] = content.Load<Texture2D>("buttons/1");
        _sprites[ButtonSprite.Two] = content.Load<Texture2D>("buttons/2");
        _sprites[ButtonSprite.Three] = content.Load<Texture2D>("buttons/3");
        _sprites[ButtonSprite.Four] = content.Load<Texture2D>("buttons/4");
        _sprites[ButtonSprite.Five] = content.Load<Texture2D>("buttons/5");
        _sprites[ButtonSprite.Six] = content.Load<Texture2D>("buttons/6");
        _sprites[ButtonSprite.Seven] = content.Load<Texture2D>("buttons/7");
        _sprites[ButtonSprite.Eight] = content.Load<Texture2D>("buttons/8");
        _sprites[ButtonSprite.Blank] = content.Load<Texture2D>("buttons/blank");
        _sprites[ButtonSprite.Bomb] = content.Load<Texture2D>("buttons/mine");
        _sprites[ButtonSprite.Flag] = content.Load<Texture2D>("buttons/flag");
    }

    public SweeperButton(Position position)
    {
        Position = position;
    }

    public Position Position { get; }
    public Vector2 VectorPosition => new (Position.Col * ButtonWidth, Position.Row * ButtonHeight);

    public void Render(SpriteBatch spriteBatch, Board board)
    {
        Texture2D texture = FindSprite(board.Examine(Position), board);
        spriteBatch.Draw(texture, VectorPosition, Color.White);
    }

    private Texture2D FindSprite(Cell cellInfo, Board board)
    {
        return (cellInfo.State, cellInfo.Contents, board.CountNeighborMines(Position)) switch
        {
            (_, _, 0) => _sprites[ButtonSprite.Blank],
            (_, _, 1) => _sprites[ButtonSprite.One],
            (_, _, 2) => _sprites[ButtonSprite.Two],
            (_, _, 3) => _sprites[ButtonSprite.Three],
            (_, _, 4) => _sprites[ButtonSprite.Four],
            (_, _, 5) => _sprites[ButtonSprite.Five],
            (_, _, 6) => _sprites[ButtonSprite.Six],
            (_, _, 7) => _sprites[ButtonSprite.Seven],
            (_, _, 8) => _sprites[ButtonSprite.Eight],
            _ => throw new System.NotImplementedException(),
        };
    }
    
}

public enum ButtonSprite
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Blank,
    Bomb,
    Flag
}