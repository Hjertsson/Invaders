using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class GUI : Entity
{
    private readonly Text scoreText;
    private int maxHealth;
    private int currentHealth;
    private int currentScore = 100;
    
    public GUI() : base("sheet")
    {
        scoreText = new Text();
        //sprite.TextureRect = new IntRect(20, 20, 18, 18);
        maxHealth = 3;
    }

    public override void Create(Scene scene)
    {
        scoreText.Font = scene.Assets.LoadFont("pixel-font");
        scoreText.CharacterSize = 100;
        scoreText.DisplayedString = "Current Score: ";
        scoreText.Scale = new Vector2f(0.1f, 0.1f);
        scoreText.FillColor = Color.White;
        
        scoreText.Position = new Vector2f(Program.SCREEN_WIDTH - scoreText.GetGlobalBounds().Width, 20);

        base.Create(scene);
    }

    public override void Render(RenderTarget target)
    {
        sprite.Position = new Vector2f(10, 10);

        for (int i = 0; i < maxHealth; i++)
        {
            sprite.TextureRect = i < currentHealth
                ? new IntRect(450, 1000, 18, 18)
                : new IntRect(430, 992, 32, 32);
                
            base.Render(target);
            sprite.Position += new Vector2f(32, 0);
        }

        scoreText.DisplayedString = $"Score: {currentScore}";
        target.Draw(scoreText);
    }
}