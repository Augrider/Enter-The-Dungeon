using System.Collections;
using System.Collections.Generic;
using Developed.SceneManagement;
using Game.Items.Components;
using Game.Player;
using Game.Plugins.ObjectPool;
using Game.SaveSystem;
using UnityEngine;

namespace Game.State
{
    public class GameStateComponent : SceneLoader, IGameState
    {
        [SerializeField] private SceneData[] _levels;

        [SerializeField] private SaveSystem.SaveComponent _saveComponent;

        public int Level { get; private set; }
        public int LevelCount => _levels.Length;


        protected override void Awake()
        {
            base.Awake();

            GameStateLocator.Provide(this);
        }

        private void OnDestroy()
        {
            GameStateLocator.Provide(null);
        }


        public void GoToLevel(int value, System.Action levelLoadedCallback = null)
        {
            Level = value;

            UnloadAll();
            LoadFromSceneData(_levels[value], doAfterLoad: levelLoadedCallback);
        }


        public void LoadPlayerState()
        {
            if (!_saveComponent.IsSaved())
            {
                Debug.LogWarning("No save detected, loading skipped");
                return;
            }

            ImportPlayer(Players.Current, _saveComponent.Load());
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

            playerData.Level = Level;

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