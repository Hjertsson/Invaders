using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public enum GameState
{
    MainMenu,
    Game
}

public sealed class Game
{
    private const float delay = 1.5f;

    private readonly AssetManager assets = new AssetManager();
    private readonly Text menuText = new Text();

    private GameState state = GameState.MainMenu; 
    private Scene scene;
    private float deathTimer;
    private int lastScore;

    public Game()
    {
        menuText.Font = assets.LoadFont("pixel-font");
        menuText.FillColor = Color.White;
    }
    
    public void Update(float dt)
    {
        switch (state)
        {
            case GameState.MainMenu:
                if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                {
                    Start();
                }

                break;
            case GameState.Game:
                scene.Update(dt);
                if (scene.GameOver)
                {
                    deathTimer += dt;
                    if (deathTimer >= delay)
                    {
                        lastScore = scene.Score;
                        state = GameState.MainMenu;
                    }
                    
                }
                break;
        }
    }

    public void Render(RenderTarget target)
    {
        switch (state)
        {
            case GameState.MainMenu:
                DrawMenu(target, "INVADERS", 250);
                DrawMenu(target, "Press Enter To Play", 400);
                if (lastScore >= 0)
                {
                    DrawMenu(target, $"Last score: {lastScore}", 460);
                }

                break;
            case GameState.Game:
                scene!.Render(target);
                break;
        }
    }

    private void Start()
    {
        scene = new Scene(assets);
        deathTimer = 0;
        state = GameState.Game;
    }

    private void DrawMenu(RenderTarget target, string text, float y)
    {
        menuText.DisplayedString = text;
        FloatRect bounds = menuText.GetLocalBounds();
        menuText.Origin = new Vector2f(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f);
        menuText.Position = new Vector2f(Program.SCREEN_WIDTH / 2f, y);
        target.Draw(menuText);
    }
    
}