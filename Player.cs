using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class Player : Ship
{
    private Vector2f playerShipSize;

    public Player()
    {
        Position = new Vector2f(Program.SCREEN_WIDTH / 2, Program.SCREEN_HEIGHT - 100);
    }
    
    
    public override void Create(Scene scene)
    {
        Speed = 100.0f;
        base.Create(scene);
        playerShipSize = (Vector2f)sprite.Texture.Size;
        sprite.TextureRect = new IntRect(0, 941, 110, 110);
        
    }
}