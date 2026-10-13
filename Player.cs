using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public sealed class Player : Ship
{
    private static readonly Vector2f GunLeft = new Vector2f(-32, -10);
    private static readonly Vector2f GunRight = new Vector2f(32, -10);

    //private const int gainedSore = 10;
    private float scoreTimer;
    
    private bool immortal = false;
    private float timer;
    
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
    }

    private bool BorderCheck(Vector2f position)
    {
        bool up = position.Y < 0 + 55;
        bool down = position.Y > Program.SCREEN_HEIGHT - 20;
        bool right = position.X > Program.SCREEN_WIDTH -55;
        bool left = position.X < 0 + 55;
        return up || down || right || left;
    }

    protected override void Move(float dt)
    {
        var newPos = sprite.Position;
        
        if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
        {
            newPos.Y -= dt * FlightSpeed;
        }
        
        if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
        {
            newPos.Y += dt * FlightSpeed;
        }

        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            newPos.X -= dt * FlightSpeed;
        }

        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            newPos.X += dt * FlightSpeed;
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
        timer = 0;
        base.TakeDamage(scene, amount);
    }

    protected override void CollideWith(Scene scene, Entity other)
    {
        if (other is Enemy enemy && !enemy.Dead)
        {
            enemy.TakeDamage(scene, 1);
            TakeDamage(scene, 1);
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
        
        
        
        timer += dt;
        if (immortal)
        {
            sprite.Color = new Color(255, 255, 255 , 100);
            if (timer >= 2)
            {
                sprite.Color = new Color(255, 255, 255);
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