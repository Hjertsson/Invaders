using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Invaders;

class Program
{
    static void Main(string[] args)
    {
        using (var window = new RenderWindow(
                   new VideoMode(828, 900), "Pacman"))
        {
            window.Closed += (o, e) => window.Close();
// TODO: Initialize
            Clock clock = new Clock();
            while (window.IsOpen)
            {
                window.DispatchEvents();
                float dt = clock.Restart().AsSeconds();
                dt = MathF.Min(dt, 0.01f);
// TODO: Updates
                window.Clear(new Color(223, 246, 245));
// TODO: Drawing
                window.Display();
            }
        }

    }
}