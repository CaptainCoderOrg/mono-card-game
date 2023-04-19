using SFML.Graphics;
using SFML.Window; 
using SFML.System;
using CaptainCoder.Core;
using CaptainCoder.MineSweeper;

VideoMode mode = new(800, 600);
RenderWindow window = new(mode, "Sweep 'Em");

Font font = new ("Assets/VT323-Regular.ttf");
Text text = new ("Sweeper", font);

Texture texture = new ("Assets/buttons/1.png");
Sprite sprite = new (texture);

// Vector2f position = new (50, 50);
text.Position = new (50, 50);

Board board = new Board(8, 10);
SweeperBoardRenderer boardRenderer = new (board);

window.MouseButtonPressed += boardRenderer.HandleMouseButtonPressed;

while (window.IsOpen)
{
    window.Clear();
    // Process events
    // sf::Event event;
    window.DispatchEvents(); 

    boardRenderer.Render(window);
    // // Finally, display the rendered frame on screen
    window.Display();
}

