namespace Game.State
{
    public interface IGameState
    {
        event System.Action<bool> PauseSet;
        bool Paused { get; }

        int Level { get; }
        int EnemiesKilled { get; set; }

        IGameStateControl Control { get; }
    }
}