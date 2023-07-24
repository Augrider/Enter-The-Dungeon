using System.Collections.Generic;
using Developed.SceneManagement;
using Game.Items.Components;
using Game.Player;
using Game.Plugins.ObjectPool;
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


        public bool IsPlayerSaved()
        {
            return _saveComponent.IsSaved();
        }

        public void LoadPlayerState(bool loadLevel = false)
        {
            if (!_saveComponent.IsSaved())
            {
                Debug.LogWarning("No save detected, loading skipped");
                return;
            }

            var saveData = _saveComponent.Load();

            if (loadLevel)
            {
                GoToLevel(saveData.Level);
                return;
            }

            ImportPlayer(Players.Current, saveData);
        }

        public void SavePlayerState()
        {
            if (_saveComponent.IsSaved())
                Debug.LogWarning("Save detected, overwriting");

            var playerData = Export(Players.Current);
            _saveComponent.Save(playerData);
        }

        public void ErasePlayerSave()
        {
            _saveComponent.ClearSave();
        }



        private PlayerData Export(IPlayer player)
        {
            var playerData = new PlayerData();

            playerData.Level = _stateComponent.Level;
            playerData.EnemiesKilled = _stateComponent.EnemiesKilled;

            playerData.Health = player.State.Health;
            playerData.Currency = player.Inventory.Currency;

            var items = player.Inventory.GetItems();

            var itemIDList = new List<int>();
            var weaponsData = new List<WeaponData>();

            foreach (var item in items)
            {
                var itemID = item.ID;
                itemIDList.Add(itemID);

                if (player.Weapons.TryGetWeapon(itemID, out var weapon))
                {
                    var weaponData = new WeaponData();

                    weaponData.ItemID = itemID;
                    weaponData.Ammo = weapon.State.TotalAmmo;

                    weaponsData.Add(weaponData);
                }
            }

            playerData.Items = itemIDList.ToArray();
            playerData.WeaponData = weaponsData.ToArray();

            return playerData;
        }

        private void ImportPlayer(IPlayer player, PlayerData playerData)
        {
            //What about current player state? Remove items? Or assume there is nothing recorded?
            player.Inventory.Currency = playerData.Currency;

            _stateComponent.EnemiesKilled = playerData.EnemiesKilled;

            foreach (var id in playerData.Items)
            {
                var item = ObjectPoolLocator.Service.GetItem(ItemDatabase.GetPrefab(id));
                Debug.Log(item);
                player.Inventory.PickItem(item);
            }

            foreach (var weaponData in playerData.WeaponData)
                if (player.Weapons.TryGetWeapon(weaponData.ItemID, out var weapon))
                    weapon.State.TotalAmmo = weaponData.Ammo;
        }
    }
}