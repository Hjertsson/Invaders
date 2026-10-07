using SFML.Graphics;

namespace Invaders;

public sealed class Scene
{
    private List<Entity> entities;
    public readonly AssetManager Assets;
    public readonly EventManager Events;

    private Player player = new Player();
    private Enemy enemy = new Enemy();
    private GUI gui = new GUI();
    private Background background = new Background();

    public Scene()
    {
        entities = new List<Entity>();
        Assets = new AssetManager();
        Events = new EventManager();

        Spawn(background);
        Spawn(gui);
        Spawn(player);
        Spawn(enemy);
        
    }

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }

    public bool FinByType<T>(out T found) where T : Entity
    {
        foreach (var entity in entities)
        {
            if (!entity.Dead && entity is T typed)
            {
                found = typed;
                return true;
            }
        }

        found = default(T);
        return false;
    }

    public IEnumerable<Entity> FindIntersects(FloatRect bounds)
    {
        int lastEntity = entities.Count - 1;

        for (int i = lastEntity; i >= 0; i--)
        {
            Entity entity = entities[i];
            if(entity.Dead) continue;
            if (entity.Bounds.Intersects(bounds))
            {
                yield return entity;
            }
        }
    }

    public void UpdateAll(float dt)
    {
        for (int i = entities.Count - 1; i >= 0 ; i--)
        {
            Entity entity = entities[i];
            entity.Update(this, dt);
        }

        for (int i = 0; i < entities.Count;)
        {
            Entity entity = entities[i];
            if(entity.Dead) entities.RemoveAt(i);
            else i++;
        }
    }
    public void RenderAll(RenderTarget target)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Render(target);
        }
    }
}