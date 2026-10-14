using System.Security.AccessControl;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace Invaders;

public sealed class Player : Ship
{
    private static readonly Vector2f GunLeft = new Vector2f(-32, -10);
    private static readonly Vector2f GunRight = new Vector2f(32, -10);

    private static readonly Color DefaultColor = Color.White;
    private static readonly Color ImmortalColor = new Color(60, 60, 60, 140);
    
    private const int EnemyShipWidth = 50;
    
    private const int damage = 1;
    
    private float scoreTimer;

    private bool immortal;
    private float immortalTimer;
    
    public Player() : base(3)
    {
        HitBoxSize = new Vector2f(70, 55);
    }
    public override void Create(Scene scene)
    {
        Position = new Vector2f(Program.SCREEN_WIDTH / 2f, Program.SCREEN_HEIGHT - 100);
        sprite.TextureRect = new IntRect(0, 941, 112, 75);
        sprite.Origin = new Vector2f(sprite.TextureRect.Width /2f, sprite.TextureRect.Height / 2f);
        base.Create(scene);
        BulletSound.Volume = 20;
    }

    private bool BorderCheck(Vector2f position)
    {
        bool up = position.Y < EnemyShipWidth + 10;
        bool down = position.Y > Program.SCREEN_HEIGHT - EnemyShipWidth + 10;
        bool right = position.X > Program.SCREEN_WIDTH -EnemyShipWidth;
        bool left = position.X < 0 + EnemyShipWidth;
        return up || down || right || left;
    }

    protected override void Move(float dt)
    {
        var newPos = sprite.Position;
        
        if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
        {
            newPos.Y -= dt * Speed;
        }
        
        if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
        {
            newPos.Y += dt * Speed;
        }

        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            newPos.X -= dt * Speed;
        }

        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            newPos.X += dt * Speed;
        }
        if (!BorderCheck(newPos))
        {
            sprite.Position = newPos;
        }
    }

    protected override void Shoot(Scene scene, Vector2f direction)
    {
        scene.Spawn(new Bullet(this, direction, Position + GunLeft));
        scene.Spawn(new Bullet(this, direction, Position + GunRight));
    }

    public override void TakeDamage(Scene scene, int amount)
    {
        if(immortal) return;
        
        scene.Events.PublishLoseHealth(amount);
        immortal = true;
        immortalTimer = 0;
        base.TakeDamage(scene, amount);
    }

    protected override void CollideWith(Scene scene, Entity other)
    {
        if (other is Enemy enemy && !enemy.Dead)
        {
            enemy.TakeDamage(scene, damage);
            TakeDamage(scene, damage);
        }
    }

    public override void Update(Scene scene, float dt)
    {
        scoreTimer += dt;
        if (scoreTimer > 1f)
        {
            scoreTimer = 0;
            scene.Events.PublishGainScore(100);
        }
        immortalTimer += dt;
        if (immortal)
        {
            sprite.Color = ImmortalColor;
            if (immortalTimer >= 2)
            {
                sprite.Color = DefaultColor;
                immortal = false;
            }
        }
        
        if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
        {
            TryShoot(scene, new Vector2f(0,-1));
        }
        Move(dt);
        base.Update(scene, dt);
    }
}