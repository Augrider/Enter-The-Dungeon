using System.Collections;
using System.Collections.Generic;
using Game.Units;
using Game.Weapons;
using UnityEngine;

namespace Game.Player.Components
{
    //Finally, the answer is simple: when player is set, change reference to components to those of a unit
    //Sharing interface helps a lot in this case

    /// <summary>
    /// A proxy class that stores state and stats as data while Player Unit is not spawned yet and updates it after spawn
    /// </summary>
    public class Player : IPlayer
    {
        private PlayerUnit _playerUnit;

        private PlayerState _playerState;
        private PlayerInventory _playerInventory;
        private PlayerWeapons _playerWeapons;

        public bool IsUnitSet { get; private set; } = false;
        public string CharacterID { get; private set; }

        public Vector3 Position => IsUnitSet ? _playerUnit.Transform.Position : Vector3.zero;

        public PlayerStats Stats
        {
            get => State.Stats;
            set => State.Stats = value;
        }

        public IPlayerState State { get; private set; }
        public IPlayerItems Inventory { get; private set; }
        public IPlayerWeapons Weapons { get; private set; }

        public IPlayerUnit PlayerUnit => _playerUnit;


        public Player()
        {
            _playerState = new PlayerState(this);
            _playerInventory = new PlayerInventory(this);
            _playerWeapons = new PlayerWeapons(this);

            State = _playerState;
            Inventory = _playerInventory;
            Weapons = _playerWeapons;

            Components.PlayerUnit.PlayerUnitSpawned += SetUnit;
        }


        public void SetUnit(PlayerUnit unit)
        {
            Debug.LogWarning("Player Unit Set");

            var data = Export();
            _playerUnit = unit;

            TogglePlayerSet(true);
            Import(data);

            PlayerEvents.InvokePlayerSpawned();
        }

        public void ResetUnit()
        {
            Debug.LogWarning("Player Unit Reset");

            var data = Export();
            _playerUnit = null;

            TogglePlayerSet(false);
            Import(data);

            PlayerEvents.InvokePlayerDied();
        }


        public void Import(PlayerFullData playerData)
        {
            CharacterID = playerData.CharacterID;

            if (IsUnitSet)
            {
                _playerUnit.Import(playerData);
                return;
            }

            _playerState.SetDefaultStats(PlayerDatabase.GetDefaultStats(CharacterID));
            ImportState(playerData.State);
        }

        public PlayerFullData Export()
        {
            if (IsUnitSet)
                return _playerUnit.Export();

            return new PlayerFullData(CharacterID, ExportState());
        }


        public void ImportState(PlayerStateData data)
        {
            State.Health = data.Health;
            State.Currency = data.Currency;

            _playerInventory.Import(data);
            _playerWeapons.Import(data);
        }

        public PlayerStateData ExportState()
        {
            var playerData = new PlayerStateData();

            playerData.Health = State.Health;
            playerData.Currency = State.Currency;

            playerData.Items = _playerInventory.Export();
            playerData.WeaponData = _playerWeapons.Export();

            return playerData;
        }



        private void TogglePlayerSet(bool value)
        {
            IsUnitSet = value;

            State = IsUnitSet ? PlayerUnit.State : _playerState;
            Inventory = IsUnitSet ? PlayerUnit.Inventory : _playerInventory;
            Weapons = IsUnitSet ? PlayerUnit.Weapons : _playerWeapons;
        }
    }
}