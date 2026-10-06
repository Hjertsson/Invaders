using SFML.System;

namespace Invaders;

public class Ship : Entity
{
    protected bool Collided;

    protected float Speed;
    protected int Direction;
    
    
    protected Ship() : base("sheet") {}
    
    
}