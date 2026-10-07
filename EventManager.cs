namespace Invaders;

public delegate void ValueChangedEvent(Scene scene, int value);

public sealed class EventManager
{
    private int scoreGain;
    private int healthLost;


    public event ValueChangedEvent GainScore;
    public event ValueChangedEvent LoseHealth;


    public void PublishGainScore(int amount) => scoreGain += amount;
    public void PublishLoseHealth(int amount) => healthLost += amount;

    public void CheckEvent(Scene scene)
    {
        if (scoreGain != 0)
        {
            GainScore?.Invoke(scene, scoreGain);
            scoreGain = 0;
        }

        if (healthLost != 0)
        {
            LoseHealth?.Invoke(scene, healthLost);
            healthLost = 0;
        }
    }
    
}