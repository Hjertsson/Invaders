
namespace Invaders;

public sealed class HighScore
{
    private const string FilePath = "highscore.txt";
    private const int PlayerLimit = 5;

    private readonly List<HighScoreEntry> highscores = new List<HighScoreEntry>();
    public List<HighScoreEntry> Highscores => highscores;
    

    public HighScore()
    {
        LoadHighScore();
    }

    public bool AddHighScore(string name, int score)
    {
        int index = 0;
        while (index < highscores.Count && highscores[index].Score >= score) //sålänge index är mer än antal highscores som redan är sparade, och Score värdet som är sparad på det indexet är störrre än det inskicade score värdet
        {
            index++;
        }

        if (index >= PlayerLimit) return false; //Om index är mer än PlayerLimit(5), dvs score har inte varit högre än något av de 5 sparade objekten så returnera false
        
        highscores.Insert(index, new HighScoreEntry(name, score)); //Lägger in det nya highscore entryn på index platsen
        if (highscores.Count > PlayerLimit) //Om detta gjorde så att det nu finns fler än 5 sparade entries, ta bort den sista i listan
        {
            highscores.RemoveAt(highscores.Count - 1);
        }
        
        Save();
        return true;
    }

    private void LoadHighScore()
    {
        if (!File.Exists(FilePath)) return; //Skippar om det är första gången

        string[] lines = File.ReadAllLines(FilePath); //Läser igenoim alla rader som finns i filen och skapar en array av dom
        for (int i = 0; i + 1 < lines.Length; i += 2) //Eftersom det sparas på 2 rader i dokumentet som hoppar det 2 platser, 
        {
            if (int.TryParse(lines[i + 1], out int score)) // i+ 1 blir då score
            {
                highscores.Add(new HighScoreEntry(lines[i], score));
            }
        }
    }

    private void Save()
    {
        List<string> lines = new List<string>();
        foreach (HighScoreEntry highscore in highscores) //För varje spara highscore i highscores
        {
            lines.Add(highscore.Name); //Så skriv name och score ut
            lines.Add(highscore.Score.ToString());
        }
        File.WriteAllLines(FilePath, lines);
    }
}