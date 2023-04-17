using CaptainCoder.Core;
using CaptainCoder.MineSweeper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CaptainCoder.MonoCards;

public class Game1 : Game
{
    private SpriteFont _gameFont;
    private Texture2D _ballTexture;
    private Vector2 _ballPosition;
    private float _ballSpeed;
    private Board _board;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<SweeperButton> _buttons;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _board = new Board(8, 12);
        _board.PlaceRandomMines((0,0), 10);
        _buttons = new();
        foreach (Position position in _board.Positions)
        {
            _buttons.Add(new SweeperButton(position));
        }
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
        _ballSpeed = 100f;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _ballTexture = Content.Load<Texture2D>("ball");
        _gameFont = Content.Load<SpriteFont>("ConsolasFont");
        SweeperButton.LoadGraphics(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        MouseState mouseState = Mouse.GetState();

        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        // _spriteBatch.Draw(_ballTexture, _ballPosition, Color.White);
        
        foreach (SweeperButton button in _buttons)
        {
            button.Render(_spriteBatch, _board);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
