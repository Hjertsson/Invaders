using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public sealed class Bullet : Entity
{
    private const float speed = 400.0f;
    private readonly Ship owner;
    private readonly Vector2f direction;
    public int Damage { get; } = 1;
    
    
    //BLue new IntRect(856, 421, 9, 54)
    //Red (858, 230, 9, 54)
    
    public Bullet(Ship owmer, Vector2f direction) : base ("sheet")
    {
        this.owner = owmer;
        this.direction = direction;
        Position = owner.Position;
    }

    public override void Create(Scene scene)
    {
        sprite.TextureRect = new IntRect(0, 0, 24, 24);
        sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);
        base.Create(scene);
    }

    protected override void Move(float dt)
    {
        Position += direction * speed * dt;
        if (Position.Y < -50 || Position.Y > Program.SCREEN_HEIGHT + 50)
        {
            Dead = true;
        }
    }
    protected override void CollideWith(Scene scene, Entity other)
    {
        if(Dead || other is not Ship ship || ship.Dead) return;
        if (ship.GetType() == owner.GetType()) return;

        ship.TakeDamage(scene, Damage);
        Dead = true;
        scene.Spawn(new Explosion(Position));
    }

    public override void Update(Scene scene, float dt)
    {
        Move(dt);
        base.Update(scene, dt);
    }
}