using SFML.Graphics;
using SFML.System;
using SFML.Audio;

namespace Invaders;

public abstract class Ship : Entity
{
    protected float ShootCooldown = 1f;
    private float shootTimer;
    protected readonly float Speed = 100.0f;
    protected int Health;
    
    protected Sound BulletSound;

    protected Vector2f HitBoxSize;

    protected Ship(int health) : base("sheet")
    {
        Health = health;
    }

    public override void Create(Scene scene)
    {
        BulletSound = new Sound(scene.Assets.LoadSound("laser4"));
        base.Create(scene);
    }


    protected void TryShoot(Scene scene, Vector2f direction)
    {
        if (shootTimer > 0) return;
        BulletSound.Play();
        Shoot(scene, direction);
        shootTimer = ShootCooldown;
    }

    protected virtual void Shoot(Scene scene, Vector2f direction)
    {
        scene.Spawn(new Bullet(this, direction, Position));
    }

    public virtual void TakeDamage(Scene scene, int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Dead = true;
            scene.Spawn(new Explosion(Position));
        }
    }

    public override FloatRect Bounds => new FloatRect(
        Position.X - HitBoxSize.X / 2f,
        Position.Y - HitBoxSize.Y / 2f,
        HitBoxSize.X,
        HitBoxSize.Y
    );

    public override void Update(Scene scene, float dt)
    {
        shootTimer -= dt;
        base.Update(scene, dt);
    }
}