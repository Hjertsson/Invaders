using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Invaders;


class Program
{
    public const int SCREEN_WIDTH = 600;
    public const int SCREEN_HEIGHT = 800;
    
    static void Main(string[] args)
    {
        Game game = new Game();
        
        using (var window = new RenderWindow(
                   new VideoMode(SCREEN_WIDTH, SCREEN_HEIGHT), "Invaders"))
        {
            window.Closed += (o, e) => window.Close();
            window.TextEntered += (o, e) => game.TextInput(e.Unicode); //Anropar ett event för att skicka med den texten som skrivs
            Clock clock = new Clock();
            while (window.IsOpen)
            {
                window.DispatchEvents();
                float dt = clock.Restart().AsSeconds();
                dt = MathF.Min(dt, 0.01f);
                game.Update(dt);
                if (game.QuitRequest) //Om Quit är true så stängs spelet av
                {
                    window.Close();
                    break;
                }
                window.Clear(new Color(0, 0, 0));

                game.Render(window);
                window.Display();
            }
        }

    }
}