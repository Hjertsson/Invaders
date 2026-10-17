using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public enum GameState
{
    MainMenu,
    Game,
    HighScore
}

public sealed class Game
{
    private const float delay = 1.5f;

    private readonly AssetManager assets = new AssetManager();
    private readonly Text menuText = new Text();
    private readonly HighScore highScore = new HighScore();

    private static readonly string[] menuOptions = {"Start Game", "High Score", "Quit Game" };
    private const int StartGame = 0;
    private const int HighScoreOption = 1;
    private const int QuitGame = 2;
    private int selected;
    
    private const float menuDelay = 0.2f;
    private float menuInputTimer;
        
    
    public bool QuitRequest { get; private set; }

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
                menuInputTimer -= dt;

                if (menuInputTimer > 0) break;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && selected > 0)
                {
                    selected--;
                    menuInputTimer = menuDelay;
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && selected < menuOptions.Length -1)
                {
                    selected++;
                    menuInputTimer = menuDelay;
                }
                break;
            case GameState.Game:
                scene.Update(dt);
                if (scene.GameOver)
                {
                    deathTimer += dt;
                    if (deathTimer >= delay)
                    {
                        highScore.AddHighScore(playerName, scene.Score);
                        state = GameState.MainMenu;
                    }
                }
                break;
            case GameState.HighScore:
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    state = GameState.MainMenu;
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
                if (playerName.Length > 0 && playerName.Length < 10)
                {
                    playerName = playerName.Substring(0, playerName.Length - 1);
                }
            }
            else if (c == '\r' && playerName.Length < 10)
            {
                SelectOption();
            }
            else if ((char.IsLetterOrDigit(c) || c == ' ') && playerName.Length < 10)
            {
                playerName += c;
            }
        }
       
    }

    private void SelectOption()
    {
        if (selected == StartGame && playerName.Length > 0)
        {
            Start();
        }
        else if (selected == HighScoreOption)
        {
            state = GameState.HighScore;
        }
        else if (selected == QuitGame)
        {
            QuitRequest = true;
        }
        
    }

    public void Render(RenderTarget target)
    {
        switch (state)
        {
            case GameState.MainMenu:
                DrawMenu(target, "INVADERS", 250);
                DrawMenu(target, $"Name: {playerName}", 320);

                if (playerName.Length == 0)
                {
                    DrawMenu(target, "Type your name", 380);
                }

                for (int i = 0; i < menuOptions.Length; i++)
                {
                    menuText.FillColor = i == selected ? Color.Yellow : Color.White;
                    DrawMenu(target, menuOptions[i], 420 + i * 50);
                }

                menuText.FillColor = Color.White;
                break;
            case GameState.Game:
                scene.Render(target);
                break;
            case GameState.HighScore:
                DrawMenu(target, "!!!High Scores!!!", 50);

                if (highScore.Highscores.Count == 0)
                {
                    DrawMenu(target, "No Score!", 375);
                }

                for (int i = 0; i < highScore.Highscores.Count; i++)
                {
                    HighScoreEntry entry = highScore.Highscores[i];
                    DrawMenu(target, $"{i + 1}. {entry.Name} {entry.Score}", 120 + i * 50);
                }
                
                
                DrawMenu(target, "Press Escape to go back", 750);
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