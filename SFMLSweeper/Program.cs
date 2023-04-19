using SFML.Graphics;
using SFML.Window;

VideoMode mode = new(800, 600);
RenderWindow window = new(mode, "Sweep 'Em");

Font font = new ("Assets/VT323-Regular.ttf");
Text text = new ("Sweeper", font);
Image image = new ("Assets/buttons/1.png");
Texture texture = new ("Assets/buttons/1.png");
Sprite sprite = new Sprite(texture);

while (window.IsOpen)
{
    // Process events
    // sf::Event event;
    window.DispatchEvents();
    // window.Draw(circle);
    window.Draw(text);
    window.Draw(sprite);
    // // Finally, display the rendered frame on screen
    window.Display();
}
