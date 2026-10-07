using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Enemy : Ship
{
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
    
    

    public override void Update(Scene scene, float dt)
    {
        var newPos = sprite.Position;
        newPos.Y += dt * FlightSpeed;
        newPos.X += dt * FlightSpeed;
        sprite.Position = newPos;
        sprite.Position = BorderCheck();
        base.Update(scene, dt);
    }
}