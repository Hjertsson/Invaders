namespace Invaders;

public sealed class SceneLoader
{
    private GUI gui;
    Player player = new Player();

    public SceneLoader()
    {
        gui = new GUI();
        
    }

    public void LoadGame(Scene scene)
    {
        scene.Spawn(player);
    }
}