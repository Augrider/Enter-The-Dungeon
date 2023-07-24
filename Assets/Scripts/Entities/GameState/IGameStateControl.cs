namespace Game.State
{
    public interface IGameStateControl
    {
        int LevelCount { get; }

        void ToMainMenu();
        void GoToLevel(int value, System.Action levelLoadedCallback = null);

        void TogglePause(bool value);

        void SavePlayerState();
        void LoadPlayerState(bool loadLevel = false);
        void ErasePlayerSave();
        bool IsPlayerSaved();
    }
}