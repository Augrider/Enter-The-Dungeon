using Game.Player;

namespace Game.State
{
    public interface IGameStateControl
    {
        int LevelCount { get; }

        void ToMainMenu();
        void GoToLevel(int value, System.Action levelLoadedCallback = null);

        void TogglePause(bool value);

        void Save();

        //TODO: Change controls to better work with player class permanence
        /// <summary>
        /// Perform full loading of player and level states
        /// </summary>
        void LoadSave();
        /// <summary>
        /// Load saved player
        /// </summary>
        void LoadPlayer();
        /// <summary>
        /// Load provided player
        /// </summary>
        void LoadPlayer(PlayerFullData playerData);

        bool IsSaved();

        void EraseSave();
    }
}