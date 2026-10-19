using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public sealed class Player : Ship
{
    private IntRect PlayerShipTexture = new IntRect(0, 941, 112, 75);
    
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
        SetTextureRect(PlayerShipTexture);
        base.Create(scene);
        BulletSound.Volume = 20;
    }

    private bool BorderCheck(Vector2f position) //Säkerställer att spelaren inte kan åka utanför någon kant på skärmen
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
        scene.Spawn(new Bullet(this, direction, Position + GunLeft)); //Skapar 2 skott, och med hjälp av gunlefy/right, på olika delar av skeppet
        scene.Spawn(new Bullet(this, direction, Position + GunRight));
    }

    public override void TakeDamage(Scene scene, int amount)
    {
        if(immortal) return; //Kollar om spelaren är odödlig
        
        scene.Events.PublishLoseHealth(amount); //Anropar eventet för looseHealth, vilket gör så att GUI's kopia minskar
        immortal = true; //Sätter spelaren till immortal
        immortalTimer = 0; //Återställer immortal timern
        base.TakeDamage(scene, amount); //Här inne minska den "riktiga" health variabeln
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
            scoreTimer -= 1f;
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