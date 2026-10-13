using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class GUI : Entity
{
    private readonly Text scoreText;
    private readonly int maxHealth;
    private int currentHealth;
    private int currentScore = 0;

    private static readonly IntRect HealthPng = new IntRect(502, 991, 36, 26);
    
    public GUI() : base("sheet")
    {
        scoreText = new Text();
        maxHealth = 3;
        currentHealth = maxHealth;
    }

    public override void Create(Scene scene)
    {
        scoreText.Font = scene.Assets.LoadFont("pixel-font");
        scoreText.CharacterSize = 100;
        scoreText.DisplayedString = "Current Score: ";
        scoreText.Scale = new Vector2f(0.1f, 0.1f);
        scoreText.FillColor = Color.White;
        
        scoreText.Position = new Vector2f(Program.SCREEN_WIDTH - scoreText.GetGlobalBounds().Width, 20);
        scene.Events.LoseHealth += onTakeDamage;
        scene.Events.GainScore += onGainScore;
        base.Create(scene);
    }

    private void onTakeDamage(Scene scene, int amount)
    {
        currentHealth -= amount;
    }

    private void onGainScore(Scene scene, int amount)
    {
        currentScore += amount;
    }

    public override void Render(RenderTarget target)
    {
        sprite.TextureRect = HealthPng;
        sprite.Position = new Vector2f(10, 10);

        for (int i = 0; i < maxHealth; i++)
        {
            sprite.Color = i < currentHealth
                ? Color.White
                : new Color(60, 60, 60, 140);
                
            base.Render(target);
            sprite.Position += new Vector2f(32, 0);
        }

        sprite.Color = Color.White;

        scoreText.DisplayedString = $"Score: {currentScore}";
        target.Draw(scoreText);
    }
}