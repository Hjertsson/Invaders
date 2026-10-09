using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public class Enemy : Ship
{
    private Vector2f direction = new Vector2f(RandomDirection(), 1);
    private Vector2f newPos;
    
    public Enemy() : base(1)
    {
        FireCooldown = 1.5f;
    }

    public override void Create(Scene scene)
    {
        sprite.Position = new Vector2f(new Random().Next(55, Program.SCREEN_WIDTH - 55),-55);
        sprite.TextureRect = new IntRect(423, 728, 93, 84);
        sprite.Origin = new Vector2f(sprite.TextureRect.Width /2f, sprite.TextureRect.Height / 2f);
        newPos = sprite.Position;
        base.Create(scene);
        
    }

    protected override void Move(float dt)
    {
        newPos += direction * dt * FlightSpeed;
        if (newPos.Y > Program.SCREEN_HEIGHT + 55)
        {
            newPos.Y = 25;
        }
        if (newPos.X < 0 + 50)
        {
            newPos.X = 55;
            Reflect(new Vector2f(1,0));
        }
        if (newPos.X > Program.SCREEN_WIDTH - 35)
        {
            newPos.X = Program.SCREEN_WIDTH - 35;
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

    public static int RandomDirection()
    {
        int angle = new Random().Next(0, 2);
        if (angle == 0)
        {
            return -1;
        }

        return 1;
    }
    private void ShipFacing()
    {
        if (direction.X < 0)
        {
            sprite.Rotation = 45f;
        }

        if (direction.X >= 1)
        {
            sprite.Rotation = 315f;
        }
    }

    public override void Update(Scene scene, float dt)
    {
        ShipFacing();
        Move(dt);
        base.Update(scene, dt);
    }
}