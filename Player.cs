using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public sealed class Player : Ship
{
    private Vector2f playerShipSize;

    public Player()
    {
    }
    public override void Create(Scene scene)
    {
        Position = new Vector2f(Program.SCREEN_WIDTH / 2, Program.SCREEN_HEIGHT - 100);
        sprite.Origin = new Vector2f(55, 55);
        sprite.TextureRect = new IntRect(0, 941, 110, 110);
        base.Create(scene);
        
    }

    private Vector2f Move(float dt)
    {
        var newPos = sprite.Position;
        
        if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
        {
            newPos.Y -= dt * FlightSpeed;
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
        {
            newPos.Y += dt * FlightSpeed;
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            newPos.X -= dt * FlightSpeed;
        } 
        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            newPos.X += dt * FlightSpeed;
        }

        return newPos;
    }

    public override void Update(Scene scene, float dt)
    {
        sprite.Position = Move(dt);
        sprite.Position = BorderCheck();
        base.Update(scene, dt);
    }
}