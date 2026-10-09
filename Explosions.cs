using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class Explosion : Entity
{
    private const float FrameTime = 0.08f;
    private const int FrameCount = 4, FrameW = 64, FrameH = 64, StartX = 0, StartY = 0; //Sheet positionen
    //Blue: (596, 961, 48, 46), (434, 325, 48, 46)
    
    //red: (580, 661, 48, 46), (602, 600, 48, 46)
    private float timer;

    public Explosion(Vector2f position) : base("sheet")
    {
        Position = position;
    }

    public override void Create(Scene scene)
    {
        sprite.TextureRect = new IntRect(StartX, StartY, FrameW, FrameH);
        sprite.Origin = new Vector2f(FrameW / 2f, FrameH / 2f);
        base.Create(scene);
    }

    public override void Update(Scene scene, float dt)
    {
        timer += dt;
        int frame = (int)(timer / FrameTime);
        if (frame >= FrameCount)
        {
            Dead = true; 
            return;
        }

        sprite.TextureRect = new IntRect(StartX + frame * FrameW, StartY, FrameW, FrameH);
    }
}