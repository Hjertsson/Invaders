using System.Xml.Linq;

namespace Invaders;

public sealed class HighScore
{
    private const string FilePath = "highscore.txt";

    public string Name { get; private set; } = "";
    public int Score { get; private set; }

    public HighScore()
    {
        LoadHighScore();
    }

    public bool AddHighScore(string name, int score)
    {
        if (score <= Score) return false;

        Name = name;
        Score = score;
        Save();
        return true;
    }

    private void LoadHighScore()
    {
        if (!File.Exists(FilePath)) return; //Skippar om det är första gången

        string[] lines = File.ReadAllLines(FilePath);
        if (lines.Length >= 2 && int.TryParse(lines[1], out int savedScore))
        {
            Name = lines[0];
            Score = savedScore;
        }
    }

    private void Save()
    {
        File.WriteAllLines(FilePath, new[] {Name, Score.ToString() });
    }
}