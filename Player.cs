using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public sealed class Player : Ship
{
    public Player() : base(3)
    {
        
    }
    public override void Create(Scene scene)
    {
        Position = new Vector2f(Program.SCREEN_WIDTH / 2, Program.SCREEN_HEIGHT - 100);
        sprite.TextureRect = new IntRect(0, 941, 112, 75);
        sprite.Origin = new Vector2f(sprite.TextureRect.Width /2f, sprite.TextureRect.Height / 2f);
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

    public override void TakeDamage(Scene scene, int amount)
    {
        scene.Events.PublishLoseHealth(1);
        base.TakeDamage(scene, amount);
    }

    public override void Update(Scene scene, float dt)
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
        {
            TryShoot(scene, new Vector2f(0,-1));
        }
        Move(dt);
        base.Update(scene, dt);
    }
}