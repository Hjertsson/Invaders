using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public enum GameState
{
    MainMenu,
    Playing
}

public sealed class Game
{
    private const float DeathDelay = 1.5f;

    private readonly AssetManager assets = new AssetManager();
    private readonly Text menuText = new Text();

    private GameState state = GameState.MainMenu;
    private Scene? scene;
    private float deathTimer;
    private int lastScore = -1;

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
                    StartGame();
                }

                break;
            case GameState.Playing:
                scene!.Update(dt);
                if (scene.IsGameOver)
                {
                    deathTimer += dt;
                    if (deathTimer >= DeathDelay)
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
                DrawCentered(target, "INVADERS", 48, 250);
                DrawCentered(target, "Press Enter To Play", 20, 400);
                if (lastScore >= 0)
                {
                    DrawCentered(target, $"Last score: {lastScore}", 20, 460);
                }

                break;
            case GameState.Playing:
                scene!.Render(target);
                break;
        }
    }

    private void StartGame()
    {
        scene = new Scene(assets);
        deathTimer = 0;
        state = GameState.Playing;
    }

    private void DrawCentered(RenderTarget target, string words, uint size, float y)
    {
        menuText.DisplayedString = words;
        menuText.CharacterSize = size;
        FloatRect bounds = menuText.GetLocalBounds();
        menuText.Origin = new Vector2f(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f);
        menuText.Position = new Vector2f(Program.SCREEN_WIDTH / 2f, y);
        target.Draw(menuText);
    }
    
}