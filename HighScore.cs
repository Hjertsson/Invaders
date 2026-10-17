
namespace Invaders;

public sealed class HighScore
{
    private const string FilePath = "highscore.txt";
    private const int PlayerLimit = 10;

    private readonly List<HighScoreEntry> highscores = new List<HighScoreEntry>();
    public List<HighScoreEntry> Highscores => highscores;
    

    public HighScore()
    {
        LoadHighScore();
    }

    public bool AddHighScore(string name, int score)
    {
        int index = 0;
        while (index < highscores.Count && highscores[index].Score >= score)
        {
            index++;
        }

        if (index >= PlayerLimit) return false;
        
        highscores.Insert(index, new HighScoreEntry(name, score));
        if (highscores.Count > PlayerLimit)
        {
            highscores.RemoveAt(highscores.Count - 1);
        }
        
        Save();
        return true;
    }

    private void LoadHighScore()
    {
        if (!File.Exists(FilePath)) return; //Skippar om det är första gången

        foreach (string line in File.ReadAllLines(FilePath))
        {
            string[] lines = line.Split(';');
            if (lines.Length >= 2 && int.TryParse(lines[1], out int score))
            {
                highscores.Add(new HighScoreEntry(lines[0], score));
            }
        }
    }

    private void Save()
    {
        List<string> lines = new List<string>();
        foreach (HighScoreEntry highscore in highscores)
        {
            lines.Add($"{highscore.Name};{highscore.Score}");
        }
        File.WriteAllLines(FilePath, lines);
    }
}