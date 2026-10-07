using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public class Enemy : Ship
{
    private Vector2f direction = new Vector2f(1,1) / MathF.Sqrt(2.0f);
    private Vector2f newPos;
    
    public Enemy()
    {
    }

    public override void Create(Scene scene)
    {
        sprite.Position = new Vector2f(new Random().Next(0, Program.SCREEN_WIDTH),100);
        sprite.TextureRect = new IntRect(0, 941, 110, 110);
        sprite.Origin = new Vector2f(55, 55);
        sprite.Rotation += 210f;
        
        base.Create(scene);
        
        
    }

    protected override void Move(float dt)
    {
        newPos += direction * dt * FlightSpeed;
        if (newPos.Y < +20)
        {
            newPos.Y = 25;
            Reflect(new Vector2f(0, 1));
        }
        if (newPos.Y > Program.SCREEN_HEIGHT)
        {
            newPos.Y = 25;
        }
        if (newPos.X < 0)
        {
            newPos.X = 0;
            Reflect(new Vector2f(1,0));
        }
        if (newPos.X > Program.SCREEN_WIDTH - 20)
        {
            newPos.X = Program.SCREEN_WIDTH - 20;
            Reflect(new Vector2f(-1, 0));
        }


        sprite.Position = newPos;
    }

    private void Reflect(Vector2f normal)
    {
        direction -= normal * (2 * (
            direction.X * normal.X +
            direction.Y * normal.Y));
    }

    public override void Update(Scene scene, float dt)
    {
        Move(dt);
        base.Update(scene, dt);
    }
}