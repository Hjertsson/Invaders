using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace Invaders;

public sealed class Bullet : Entity
{
    private const float speed = 400.0f;
    private readonly Ship currentEntity;
    private readonly Vector2f direction;

    private const int borderOffset = 50;

    private static readonly IntRect PlayerBullet = new IntRect(856, 421, 9, 54);
    private static readonly IntRect EnemyBullet = new IntRect(858, 230, 9, 54);

    private readonly float rotationRight = 135f;
    private readonly float rotationLeft = 225f;

    private const int damage = 1;
    
    
    public Bullet(Ship entity, Vector2f direction, Vector2f spawnPosition) : base ("sheet") //Objektet som skapas skickar med sig själv som "entity"
    {
        currentEntity = entity;
        this.direction = direction;
        Position = spawnPosition;
    }

    public override void Create(Scene scene) //Skapar bullet, kollar först om entity som skickades med i konstruktorn är en player
    {
        
        SetTextureRect(currentEntity is Player ? PlayerBullet : EnemyBullet);
        
        if (direction.Y > 0 && direction.X > 0) // Kollar om direction X är mer eller mindre än 0, och sätter rotation för Spriten för att den ska peka rätt
        {
            sprite.Rotation = rotationRight;
        }
        else if (direction.Y > 0 && direction.X < 0)
        {
            sprite.Rotation = rotationLeft;
        }

        base.Create(scene);
    }

    protected override void Move(float dt)
    {
        Position += direction * speed * dt;
        if (Position.Y < - borderOffset || Position.Y > Program.SCREEN_HEIGHT + borderOffset)
        {
            Dead = true;
        }
    }
    protected override void CollideWith(Scene scene, Entity other)
    {
        if(Dead || other is not Ship ship || ship.Dead) return; //Kollar om Bullet är död, eller kollisionen skedde med ett objekt som inte är ett "Ship", eller om de kollderade med ett dött skepp, siåfall returneras det direkt
        if (ship.GetType() == currentEntity.GetType()) return; //Kollar om skepet som skickades in i konstruktorn är samma som objektet som kolliderades med, isåfall returnerar det direkt

        ship.TakeDamage(scene, damage);
        Dead = true;
    }

    public override void Update(Scene scene, float dt)
    {
        Move(dt);
        base.Update(scene, dt);
    }
}