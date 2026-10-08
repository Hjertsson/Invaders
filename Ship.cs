using SFML.System;

namespace Invaders;

public class Ship : Entity
{
    protected bool Collided;

    //protected Vector2f Direction = new Vector2f(0,0);
    protected readonly float FlightSpeed = 100.0f;
    
    
    protected Ship() : base("sheet") {}

    protected void ShipFacing(Vector2f direction)
    {
        if (direction.X < 0)
        {
            sprite.Rotation = 210f;
        }

        if (direction.X >= 1)
        {
            sprite.Rotation = 110;
        }
        
        
    }
    
    
}