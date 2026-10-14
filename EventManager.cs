namespace Invaders;

public delegate void ValueChangedEvent(Scene scene, int value);

public sealed class EventManager
{
    private int scoreGain;
    private int healthLost;


    public event ValueChangedEvent OnGainScore;
    public event ValueChangedEvent OnLoseHealth;


    public void PublishGainScore(int amount) => scoreGain += amount;
    public void PublishLoseHealth(int amount) => healthLost += amount;

    public void CheckEvent(Scene scene)
    {
        if (scoreGain != 0)
        {
            OnGainScore?.Invoke(scene, scoreGain);
            scoreGain = 0;
        }

        if (healthLost != 0)
        {
            OnLoseHealth?.Invoke(scene, healthLost);
            healthLost = 0;
        }
    }
    
}