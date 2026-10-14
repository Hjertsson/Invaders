using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class GUI
{
    private readonly Text scoreText;
    private readonly int maxHealth;
    private int currentHealth;
    private int currentScore = 0;

    private Sprite GUISprite = new Sprite();

    public int Score => currentScore; //Ger Scene access till Score
    
    private static readonly IntRect HealthPng = new IntRect(502, 991, 36, 26);
    
    public GUI()
    {
        scoreText = new Text();
        maxHealth = 3;
        currentHealth = maxHealth;
        
    }

    public void Create(Scene scene)
    {
        scoreText.Font = scene.Assets.LoadFont("pixel-font");
        scoreText.CharacterSize = 100;
        scoreText.DisplayedString = "Current Score: ";
        scoreText.Scale = new Vector2f(0.1f, 0.1f);
        scoreText.FillColor = Color.White;
        scoreText.Position = new Vector2f(Program.SCREEN_WIDTH - scoreText.GetGlobalBounds().Width, 20);

        GUISprite.Texture = scene.Assets.LoadTexture("sheet");
        
        scene.Events.OnLoseHealth += onTakeDamage;
        scene.Events.OnGainScore += onGainScore;
    }

    private void onTakeDamage(Scene scene, int amount)
    {
        currentHealth -= amount;
    }

    private void onGainScore(Scene scene, int amount)
    {
        currentScore += amount;
    }

    public void Render(RenderTarget target)
    {
        GUISprite.TextureRect = HealthPng;
        GUISprite.Position = new Vector2f(10, 10);

        for (int i = 0; i < maxHealth; i++)
        {
            GUISprite.Color = i < currentHealth
                ? Color.White
                : new Color(60, 60, 60, 140);
                
            target.Draw(GUISprite);
            GUISprite.Position += new Vector2f(40, 0);
        }

        GUISprite.Color = Color.White;

        scoreText.DisplayedString = $"Score: {currentScore}";
        target.Draw(scoreText);
    }
}