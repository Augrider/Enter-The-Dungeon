using System;
using System.Collections.Generic;
using System.Linq;
using Game.Items.Components;
using Game.Units.Components;
using Game.Weapons.Components;
using UnityEngine;

namespace Game.Player.Components
{
    [CreateAssetMenu(menuName = "Player/Data")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private PlayerUnit _unit;
        [Space]
        [Header("Unit Stats")]
        [SerializeField] private PlayerStats _defaultStats;

        [Space]
        [Header("Starts with")]
        [SerializeField] private int _startingCurrency;

        [SerializeField] private Item _startingWeapon;
        [SerializeField] private Item[] _startingItems;

        public string ID { get; internal set; }

        //String - ID for unit
        //Selected player data should be stored somewhere
        //Starting weapon should be created before putting it on the unit
        //Player should receive it's copy of save
        public PlayerUnit PlayerUnitPrefab => _unit;


        public PlayerFullData GetFullData()
        {
            var playerData = new PlayerFullData(ID, GetInitialState());
            return playerData;
        }

        public PlayerStats GetDefaultStats()
        {
            return _defaultStats;
        }

        public PlayerStateData GetInitialState()
        {
            var playerState = new PlayerStateData();

            playerState.Health = _defaultStats.MaxHealth;
            playerState.Currency = _startingCurrency;

            var itemsList = new List<Item>(_startingItems.Length + 1);
            itemsList.Add(_startingWeapon);
            itemsList.AddRange(_startingItems);

            if (itemsList.Count > 0)
                playerState.Items = itemsList.Select(t => t.ID).ToArray();
            else
                playerState.Items = new string[0];

            playerState.WeaponData = new Weapons.WeaponData[0];

            return playerState;
        }
    }
}