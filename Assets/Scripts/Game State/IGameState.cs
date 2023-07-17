namespace Game.State
{
    public interface IGameState
    {
        int Level { get; }
        int LevelCount { get; }

        void GoToLevel(int value, System.Action levelLoadedCallback = null);

        void SavePlayerState();
        void LoadPlayerState();
        void ErasePlayerSave();
    }
}