using SFML.System;

namespace Invaders;

public class Ship : Entity
{
    protected bool Collided;

    //protected Vector2f Direction = new Vector2f(0,0);
    protected readonly float FlightSpeed = 300.0f;
    
    
    protected Ship() : base("sheet") {}

    
    
    
}