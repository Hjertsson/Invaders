namespace Invaders;

public sealed class HighScoreEntry //Eftersom en lista bara kan hantera 1 objekt, så anänder jag denna klassen för att kuna skapa en lista med objekt som har 2 vörden
{
    public string Name { get;  }
    public int Score { get;  }

    public HighScoreEntry(string name, int score)
    {
        Name = name;
        Score = score;
    }
}