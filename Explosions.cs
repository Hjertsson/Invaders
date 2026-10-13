using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class Explosion : Entity
{
    private const float explosionDuration = 0.3f;
    private float timer;

    public Explosion(Vector2f position) : base("sheet")
    {
        Position = position;
    }

    public override void Create(Scene scene)
    {
        sprite.TextureRect = new IntRect(580, 661, 48, 46);
        sprite.Origin = new Vector2f(24,23);
        base.Create(scene);
    }

    public override void Update(Scene scene, float dt)
    {
        timer += dt;
        if (timer >= explosionDuration)
        {
            Dead = true;
        }

    }
}