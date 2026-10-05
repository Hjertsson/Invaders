using System.Runtime.InteropServices;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class Entity
{
    private readonly string TextureName;
    protected readonly Sprite sprite;
    public bool dead;

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

    public virtual Create(Scene scene)
    {
        sprite.Texture = scene.Assets.LoadTexture(TextureName);
    }

}