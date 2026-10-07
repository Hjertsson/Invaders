using SFML.System;

namespace Invaders;

public class Ship : Entity
{
    protected bool Collided;

    //protected Vector2f Direction = new Vector2f(0,0);
    protected readonly float FlightSpeed = 100.0f;
    
    
    protected Ship() : base("sheet") {}

    protected Vector2f BorderCheck()
    {
        var temp = sprite.Position;
        if (temp.X > Program.SCREEN_WIDTH)
        {
            Console.WriteLine("Right");
        }
        if (temp.X < 0 )
        {
            Console.WriteLine("Left");
        }
        if (temp.Y > Program.SCREEN_HEIGHT)
        {
            Console.WriteLine("Floor");
        }
        if (temp.Y < 0)
        {
            Console.WriteLine("Top");
           
        }
        return temp;
    }
    
    
}