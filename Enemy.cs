using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public class Enemy : Ship
{
    private Vector2f direction = new Vector2f(RandomDirection(), 1);
    private Vector2f newPos;
    
    public Enemy()
    {
    }

    public override void Create(Scene scene)
    {
        sprite.Position = new Vector2f(new Random().Next(55, Program.SCREEN_WIDTH - 55),-55);
        sprite.TextureRect = new IntRect(0, 941, 110, 110);
        sprite.Origin = new Vector2f(55, 55);
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

    public override void Update(Scene scene, float dt)
    {
        ShipFacing(direction);
        Move(dt);
        base.Update(scene, dt);
    }
}