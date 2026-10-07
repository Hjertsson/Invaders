using System.Runtime.InteropServices;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class Entity
{
    private readonly string TextureName;
    protected readonly Sprite sprite;
    public bool Dead;

    protected Entity(string textureName)
    {
        this.TextureName = textureName;
        sprite = new Sprite();
    }

    public Vector2f Position
    {
        get => sprite.Position;
        set => sprite.Position = value;
    }

    public virtual FloatRect Bounds => sprite.GetGlobalBounds();

    public virtual void Create(Scene scene)
    {
        sprite.Texture = scene.Assets.LoadTexture(TextureName);
    }
    
    protected virtual void Move(float dt) {}

    public virtual void Destroy(Scene scene) {}
    
    protected virtual void CollideWith(Scene scene, Entity other) {}

    public virtual void Update(Scene scene, float dt)
    {
        foreach (Entity found in scene.FindIntersects(Bounds))
        {
            CollideWith(scene, found);
        }
    }
    public virtual void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }
    
}