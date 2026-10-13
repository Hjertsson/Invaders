using SFML.Graphics;
namespace Invaders;

public sealed class EnemyManager : Entity
{
    private const float DefaultSpawnTimer = 3.0f;
    private const float MinimumSpawnTimer = 0.5f;
    private const float TimerIncrease = 0.95f;

    private float spawnTimer = DefaultSpawnTimer;
    private float timer;
    
    public EnemyManager() : base("sheet"){}

    public override void Update(Scene scene, float dt)
    {
        timer += dt;

        if (timer >= spawnTimer)
        {
            scene.Spawn(new Enemy());
            timer = 0;
            spawnTimer = MathF.Max(MinimumSpawnTimer, spawnTimer * TimerIncrease);
        }
    }
    public override void Render(RenderTarget target) {}
}