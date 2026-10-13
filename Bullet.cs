using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public sealed class Bullet : Entity
{
    private const float speed = 400.0f;
    private readonly Ship currentEntity;
    private readonly Vector2f direction;

    private int borderOffset = 50;

    private static readonly IntRect PlayerBullet = new IntRect(856, 421, 9, 54);
    private static readonly IntRect EnemyBullet = new IntRect(858, 238, 9, 54);
    //private int damage { get; } = 1;

    private readonly int damage = 1;
    
    public Bullet(Ship entity, Vector2f direction, Vector2f spawnPosition) : base ("sheet")
    {
        currentEntity = entity;
        this.direction = direction;
        Position = spawnPosition;
    }

    public override void Create(Scene scene)
    {
        sprite.TextureRect = currentEntity is Player
            ? PlayerBullet
            : EnemyBullet;
        sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2f, sprite.TextureRect.Height / 2f);

        if (direction.Y > 0 && direction.X > 0)
        {
            sprite.Rotation = 135f;
        }
        else if (direction.Y > 0 && direction.X < 0)
        {
            sprite.Rotation = 225f;
        }
        
        base.Create(scene);
    }

    protected override void Move(float dt)
    {
        Position += direction * speed * dt;
        if (Position.Y < - borderOffset || Position.Y > borderOffset)
        {
            Dead = true;
        }
    }
    protected override void CollideWith(Scene scene, Entity other)
    {
        if(Dead || other is not Ship ship || ship.Dead) return;
        if (ship.GetType() == currentEntity.GetType()) return;

        ship.TakeDamage(scene, damage);
        Dead = true;
    }

    public override void Update(Scene scene, float dt)
    {
        Move(dt);
        base.Update(scene, dt);
    }
}