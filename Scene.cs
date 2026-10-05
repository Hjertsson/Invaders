namespace Invaders;

public sealed class Scene
{
    private List<Entity> entities;
    public readonly SceneLoader Loader;
    public readonly AssetManager Assets;
    public readonly EventManager Events;

    public Scene()
    {
        entities = new List<Entity>();
        Loader = new SceneLoader();
        Assets = new AssetManager();
        Events = new EventManager();
    }

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }
}