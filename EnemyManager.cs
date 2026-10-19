namespace Invaders;

public sealed class EnemyManager
{
    private const float StartSpawnTimer = 3.0f;
    private const float MinSpawnTimer = 0.5f;
    private const float TimerIncrease = 0.95f;

    private float spawnTimer = StartSpawnTimer;
    private float timer;
    
    public void Update(Scene scene, float dt)
    {
        timer += dt;

        if (timer >= spawnTimer)
        {
            scene.Spawn(new Enemy());
            timer -= spawnTimer; 
            spawnTimer = MathF.Max(MinSpawnTimer, spawnTimer * TimerIncrease); //Max returnerar det största av 2 tal för att se till så att det inte kan spawna för snabt
        }
    }
}