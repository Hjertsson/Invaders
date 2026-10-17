using SFML.Audio;
using SFML.Graphics;

namespace Invaders;

public sealed class AssetManager
{
    private static readonly string AssetPath = "assets";
    private readonly Dictionary<string, Texture> textures;
    private readonly Dictionary<string, Font> fonts;
    private readonly Dictionary<string, SoundBuffer> sounds;

    public AssetManager()
    {
        textures = new Dictionary<string, Texture>();
        fonts = new Dictionary<string, Font>();
        sounds = new Dictionary<string, SoundBuffer>();
    }

    public SoundBuffer LoadSound(string name)
    {
        if (sounds.TryGetValue(name, out SoundBuffer found)) return found;
        SoundBuffer buffer = new SoundBuffer($"{AssetPath}/{name}.wav");
        sounds.Add(name, buffer);
        return buffer;
    }
    public Texture LoadTexture(string name)
    {
        if (textures.TryGetValue(name, out Texture found)) return found;
        Texture texture = new Texture($"{AssetPath}/{name}.png");
        textures.Add(name, texture);

        return texture;
    }

    public Font LoadFont(string name)
    {
        if (fonts.TryGetValue(name, out Font found)) return found;
        Font font = new Font($"{AssetPath}/{name}.ttf");
        fonts.Add(name, font);
        
        return font;
    }
}