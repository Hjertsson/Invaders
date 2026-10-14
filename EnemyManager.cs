using SFML.Graphics;
namespace Invaders;

public sealed class EnemyManager
{
    private const float DefaultSpawnTimer = 3.0f;
    private const float MinimumSpawnTimer = 0.5f;
    private const float TimerIncrease = 0.95f;

    private float spawnTimer = DefaultSpawnTimer;
    private float timer;
    
    public EnemyManager(){}

    public void Update(Scene scene, float dt)
    {
        timer += dt;

        if (timer >= spawnTimer)
        {
            scene.Spawn(new Enemy());
            timer -= spawnTimer;
            spawnTimer = MathF.Max(MinimumSpawnTimer, spawnTimer * TimerIncrease);
        }
    }
}