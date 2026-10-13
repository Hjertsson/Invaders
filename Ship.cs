using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class Ship : Entity
{
    protected bool Collided;

    protected float FireCooldown = 0.5f;
    private float fireTimer;
    protected readonly float FlightSpeed = 100.0f;
    protected int Health;

    protected Vector2f HitBoxSize;

    protected Ship(int health) : base("sheet")
    {
        Health = health;
    }


    protected void TryShoot(Scene scene, Vector2f direction)
    {
        if (fireTimer > 0) return;
        Shoot(scene, direction);
        fireTimer = FireCooldown;
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
        fireTimer -= dt;
        base.Update(scene, dt);
    }
}