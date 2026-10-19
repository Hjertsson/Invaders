using SFML.Graphics;
using SFML.System;


namespace Invaders;

public sealed class Enemy : Ship
{
    private Vector2f direction;
    private Vector2f newPos;
    private const int enemyShipWidth = 70;

    private IntRect EnemyShipTexture = new IntRect(423, 728, 93, 84);
    
    public Enemy() : base(1)
    {
        HitBoxSize = new Vector2f(70, 60);
        ShootCooldown = 1.5f;
        direction = new Vector2f(RandomDirection(), 1) / MathF.Sqrt(2.0f);
    }

    public override void Create(Scene scene)
    {
        sprite.Position = new Vector2f(new Random().Next(55, Program.SCREEN_WIDTH - 55), - enemyShipWidth);
        SetTextureRect(EnemyShipTexture);
        newPos = sprite.Position;
        base.Create(scene);
        BulletSound.Volume = 10;
    }

    protected override void Shoot(Scene scene, Vector2f direction)
    {
        base.Shoot(scene, direction); //anropar Shoot och skickar vidare detta enemy objektets direction
        ShootCooldown = 1f + new Random().Next(0, 2); //Slumpar hur länge enemy väntar på att skjuta, men aldrig mindre än sekund
    }

    protected override void Move(float dt)
    {
        newPos += direction * dt * Speed;
        if (newPos.Y > Program.SCREEN_HEIGHT + enemyShipWidth)
        {
            newPos.Y = -enemyShipWidth;
        }
        if (newPos.X < enemyShipWidth)
        {
            newPos.X = enemyShipWidth;
            Reflect(new Vector2f(1,0));
        }
        if (newPos.X > Program.SCREEN_WIDTH - enemyShipWidth)
        {
            newPos.X = Program.SCREEN_WIDTH - enemyShipWidth;
            Reflect(new Vector2f(-1, 0));
        }
        sprite.Position = newPos;
    }

    private void Reflect(Vector2f normal) //Samma som i breakout tutorialen
    {
        direction -= normal * (2 * (
            direction.X * normal.X +
            direction.Y * normal.Y));
    }

    public static int RandomDirection() //Slumpar vilken håll enemy börjar åka mot
    {
        int angle = new Random().Next(0, 2);
        if (angle == 0)
        {
            return -1;
        }

        return 1;
    }
    private void ShipFacing() //Ser till så att enemy texturen pekar åt rätt håll
    {
        if (direction.X < 0)
        {
            sprite.Rotation = 45f;
        }

        if (direction.X >= 1)
        {
            sprite.Rotation = 315f;
        }
    }

    public override void Update(Scene scene, float dt)
    {
        ShipFacing();
        Move(dt);
        TryShoot(scene, direction);
        base.Update(scene, dt);
    }
}