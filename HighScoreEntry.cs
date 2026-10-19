namespace Invaders;

public sealed class HighScoreEntry
{
    public string Name { get;  }
    public int Score { get;  }

    public HighScoreEntry(string name, int score)
    {
        Name = name;
        Score = score;
    }
}