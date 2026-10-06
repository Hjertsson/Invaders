using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class Player : Ship
{

    public Player()
    {
        Position = new Vector2f(Program.SCREEN_WIDTH / 2, Program.SCREEN_HEIGHT - 20);
    }
    
    
    public override void Create(Scene scene)
    {
        Speed = 100.0f;
        base.Create(scene);
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
    }
}