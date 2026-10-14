using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class Scene
{
    private List<Entity> entities;
    public readonly AssetManager Assets;
    public readonly EventManager Events;
    private readonly EnemyManager enemySpawner = new EnemyManager();
    private readonly Sprite backgroundSprite = new Sprite();

    private Player player = new Player();
    private GUI gui = new GUI();

    public bool GameOver => player.Dead; //Ger Game access till när spelaren är död
    public int Score => gui.Score; //Ger Game access til score

    public Scene(AssetManager assets)
    {
        entities = new List<Entity>();
        Assets = assets;
        Events = new EventManager();

        backgroundSprite.Texture = Assets.LoadTexture("purple");
        backgroundSprite.Texture.Repeated = true;
        backgroundSprite.TextureRect = new IntRect(0, 0, Program.SCREEN_WIDTH, Program.SCREEN_HEIGHT);
        
        gui.Create(this);
        Spawn(player);
    }

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
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

    public void Update(float dt)
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
        
        enemySpawner.Update(this, dt);
        Events.CheckEvent(this);
    }
    
    public void Render(RenderTarget target)
    {
        target.Draw(backgroundSprite);
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Render(target);
        }
        gui.Render(target);
    }
}