using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public sealed class Player : Ship
{
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

    private bool BorderCheck(Vector2f position)
    {
        bool up = position.Y < 0 + 55;
        bool down = position.Y > Program.SCREEN_HEIGHT - 20;
        bool right = position.X > Program.SCREEN_WIDTH -55;
        bool left = position.X < 0 + 55;
        return up || down || right || left;
    }

    protected override void Move(float dt)
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
        if (!BorderCheck(newPos))
        {
            sprite.Position = newPos;
        }
    }

    public override void Update(Scene scene, float dt)
    {
        Move(dt);
        base.Update(scene, dt);
    }
}