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
    private const int HighScoreOption = 1; //Sätter ett int värde på alla 3 meny alternativ
    private const int QuitGame = 2;
    private int selected; //0 som default
    
    private const float MenuDelay = 0.2f;
    private float menuInputTimer;
    
    public bool QuitRequest { get; private set; } //Gör så att program kan få tillgång till denna infon. Värdet kan enbart ändras i Game klassen

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
                menuInputTimer -= dt; // Behövs för att kunna stega igenom menyn och inte hoppa över mitten

                if (menuInputTimer > 0) break;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && selected > 0) //Om spelaren klickar pil upp, och selected är högre än 0
                {
                    selected--; //Minskar selected
                    menuInputTimer = MenuDelay; //sätter delay på nästa upp/ner input
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && selected < menuOptions.Length -1) //Om spelaren klickar pil ner, och selected är lägre än 0
                {
                    selected++;
                    menuInputTimer = MenuDelay;
                }
                break;
            case GameState.Game:
                scene.Update(dt);
                if (scene.GameOver)
                {
                    deathTimer += dt;
                    if (deathTimer >= delay) //Gör så att spelar inte direkt hamnar i menyn
                    {
                        highScore.AddHighScore(playerName, scene.Score); //Skickar in spelaren namn och vad för score spelaren hade. Score hämtas in från scene
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
        scene = new Scene(assets); //Skapar ett nytt Scene objekt
        deathTimer = 0;
        state = GameState.Game;
    }

    public void OnTextInput(string text)
    {
        if (state != GameState.MainMenu) return; // Skippar att kolla inputs om spelaren inte är i menyn

        foreach (char c in text) //Stegar igenom hela strängen som skickats in
        {
            if(Keyboard.IsKeyPressed(Keyboard.Key.Backspace))
            {
                if (playerName.Length > 0 ) //Om spelarens namn är längre än 0
                {
                    playerName = playerName.Substring(0, playerName.Length - 1); //Tar bort den sista bokstaven/siffran
                }
            }
            else if(Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                SelectOption(); //Om spelaren klickar på enter så anropas SelectOption
            }
            else if ((char.IsLetterOrDigit(c) || c == ' ') && playerName.Length < 10) // Kollar om "karaktären" som skickats in är en bokstav eller ett nummer eller tom, och om playerName redan är 10 lång
            {
                playerName += c; //Lägger till "karaktären"
            }
        }
       
    }

    private void SelectOption()
    {
        if (selected == StartGame && playerName.Length > 0) // kollar om selected är 0 (StartGame variabeln) och namnet är längre än 0
        {
            Start(); //Anropar Start
        }
        else if (selected == HighScoreOption) //kollar om selected är 1 (HighScoreOption variabeln)
        {
            state = GameState.HighScore;
        }
        else if (selected == QuitGame) //kollar om selected är 2 (QuitGame variabeln)
        {
            QuitRequest = true;
        }
        
    }

    public void Render(RenderTarget target)
    {
        switch (state) //Ett switch case för att hantera vad som ska renderas beroende på vilket state som Game är i
        {
            case GameState.MainMenu:
                DrawMenu(target, "INVADERS", 250);
                DrawMenu(target, $"Name: {playerName}", 320);

                if (playerName.Length == 0)
                {
                    DrawMenu(target, "Type your name", 380);
                }

                for (int i = 0; i < menuOptions.Length; i++) //Denna for loop stegar igenom menuOptions listan och kollar om det är Selected, isåfall ändras fill color till gul, annars vit
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

                for (int i = 0; i < highScore.Highscores.Count; i++) //Ritar ut alla sparade highscores
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