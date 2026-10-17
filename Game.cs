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
    private readonly HighScore highScore = new HighScore();
    
    private GameState state = GameState.MainMenu;

    private string playerName = "";
    private Scene scene;
    private float deathTimer;


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
                
                break;
            case GameState.Game:
                scene.Update(dt);
                if (scene.GameOver)
                {
                    deathTimer += dt;
                    if (deathTimer >= delay)
                    {
                        state = GameState.MainMenu;
                    }
                }
                break;
        }
    }
    private void Start()
    {
        scene = new Scene(assets);
        deathTimer = 0;
        state = GameState.Game;
    }

    public void OnTextInput(string text)
    {
        if (state != GameState.MainMenu) return; // Skippar att kolla inputs om spelaren inte är i menyn

        foreach (char c in text)
        {
            if (c == '\b')
            {
                if (playerName.Length > 0)
                {
                    playerName = playerName.Substring(0, playerName.Length - 1);
                }
            }
            else if (c == '\r')
            {
                if (playerName.Length > 0)
                {
                    Start();
                }
            }
            else if ((char.IsLetterOrDigit(c) || c == ' '))
            {
                playerName += c;
            }
        }
       
    }

    public void Render(RenderTarget target)
    {
        switch (state)
        {
            case GameState.MainMenu:
                DrawMenu(target, "INVADERS", 250);
                DrawMenu(target, $"Name: {playerName}", 320);
                DrawMenu(target, playerName.Length > 0 ? "Press Enter to Play" : "Type your name", 380);
                
                DrawMenu(target, $"Current HighScore: {highScore.Name} {highScore.Score}", 480);

                break;
            case GameState.Game:
                scene.Render(target);
                break;
        }
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