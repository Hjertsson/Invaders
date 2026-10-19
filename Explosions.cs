using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class Explosion : Entity
{
    private const float explosionDuration = 0.3f;
    private float timer;
    private Sound ExplosionSound;

    private IntRect ExplosionTexture = new IntRect(580, 661, 48, 46);

    public Explosion(Vector2f position) : base("sheet")
    {
        Position = position;
    }

    public override void Create(Scene scene)
    {
        SetTextureRect(ExplosionTexture);
        ExplosionSound = new Sound(scene.Assets.LoadSound("8bit_bomb")); //Laddar in ljudet
        ExplosionSound.Volume = 5; //Sätter volymen
        ExplosionSound.Play(); //Spelar ljudet samtidigt som objektet skapas
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