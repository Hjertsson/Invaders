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
        Scene scene = new Scene();
        
        using (var window = new RenderWindow(
                   new VideoMode(SCREEN_WIDTH, SCREEN_HEIGHT), "Invaders"))
        {
            window.Closed += (o, e) => window.Close();
// TODO: Initialize
            Clock clock = new Clock();
            while (window.IsOpen)
            {
                window.DispatchEvents();
                float dt = clock.Restart().AsSeconds();
                dt = MathF.Min(dt, 0.01f);
                
                scene.UpdateAll(dt);
                
                window.Clear(new Color(0, 0, 0));

                scene.RenderAll(window);
                window.Display();
            }
        }

    }
}