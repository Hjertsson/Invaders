using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class Explosion : Entity
{
    private const float explosionDuration = 0.3f;
    private float timer;
    private Sound ExplosionSound;

    private IntRect ExplosionEffect = new IntRect(580, 661, 48, 46);

    public Explosion(Vector2f position) : base("sheet")
    {
        Position = position;
    }

    public override void Create(Scene scene)
    {
        SetTextureRect(ExplosionEffect);
        ExplosionSound = new Sound(scene.Assets.LoadSound("8bit_bomb"));
        ExplosionSound.Volume = 5;
        ExplosionSound.Play();
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