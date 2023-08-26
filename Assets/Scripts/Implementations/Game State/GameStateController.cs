using Developed.SceneManagement;
using Game.Player;
using Game.SaveSystem;
using UnityEngine;

namespace Game.State.Components
{
    public class GameStateController : SceneLoader, IGameStateControl
    {
        private enum SceneState { MainMenu, Game }

        public int LevelCount => _levels.Length;

        [SerializeField] private GameStateComponent _stateComponent;
        [SerializeField] private SaveSystem.SaveComponent _saveComponent;

        [SerializeField] private ScenePackage _mainMenu;

        [SerializeField] private ScenePackage _levelAdditionalScenes;
        [SerializeField] private SceneData[] _levels;

        private SceneState _currentSceneState;


        public void ToMainMenu()
        {
            // if (_currentSceneState == SceneState.MainMenu)
            //     return;

            _currentSceneState = SceneState.MainMenu;

            UnloadAll();

            TogglePause(false);
            LoadFromScenePackage(_mainMenu);
        }


        public void GoToLevel(int value, System.Action levelLoadedCallback = null)
        {
            _currentSceneState = SceneState.Game;
            _stateComponent.Level = value;

            UnloadAll();

            TogglePause(false);
            LoadFromScenePackage(_levelAdditionalScenes);
            LoadFromSceneData(_levels[value], doAfterLoad: levelLoadedCallback);
        }

        public void TogglePause(bool value)
        {
            _stateComponent.Paused = value;
            _stateComponent.InvokePauseSet(value);

            Time.timeScale = value ? 0f : 1;
        }

        [ContextMenu("Switch pause")]
        public void SwitchPause()
        {
            _stateComponent.Paused = !_stateComponent.Paused;
            _stateComponent.InvokePauseSet(_stateComponent.Paused);
        }


        [ContextMenu("Save Game")]
        public void Save()
        {
            var saveData = Export(Players.Current);
            _saveComponent.Save(saveData);
        }


        public void LoadSave()
        {
            if (!TryLoadSave(out var saveData))
                return;

            Import(saveData);
            GoToLevel(saveData.Level);
        }

        [ContextMenu("Load Saved Player")]
        public void LoadPlayer()
        {
            if (!TryLoadSave(out var saveData))
                return;

            Import(saveData);
        }

        public void LoadPlayer(PlayerFullData playerData)
        {
            Players.Current.Import(playerData);
        }


        public bool IsSaved()
        {
            return _saveComponent.IsSaved();
        }


        public void EraseSave()
        {
            _saveComponent.ClearSave();
        }



        private bool TryLoadSave(out SaveData saveData)
        {
            saveData = new SaveData();

            if (!_saveComponent.IsSaved())
            {
                Debug.LogWarning("No save detected, loading skipped");
                return false;
            }

            saveData = _saveComponent.Load();
            return true;
        }


        private void Import(SaveData saveData)
        {
            Players.Current.Import(saveData.Player);

            _stateComponent.EnemiesKilled = saveData.EnemiesKilled;
        }

        private SaveData Export(IPlayer player)
        {
            var gameData = new SaveData();

            gameData.Level = _stateComponent.Level;
            gameData.EnemiesKilled = _stateComponent.EnemiesKilled;

            //ID for player should be taken from somewhere
            //Either from game state or Players system
            //I decided to use Player to store ID, which should be accessed by other systems (no automation inside)
            //Using ID we can get prefab and other cosmetic data (name, mini icon...) by asking database

            gameData.Player = player.Export();

            return gameData;
        }
    }
}