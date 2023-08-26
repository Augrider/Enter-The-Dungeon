using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.Components
{
    public class PlayerUnitState : IPlayerState
    {
        private IPlayer Player { get; }

        private PlayerStats _playerStatsDefault;
        private PlayerStats _playerStatsDelta;

        private int _health;
        private int _currency = 0;


        public PlayerStats Stats
        {
            get => _playerStatsDefault + _playerStatsDelta;
            set => SetStats(value - _playerStatsDefault);
        }

        public int Health { get => _health; set => SetHealth(value); }
        public int Currency { get => _currency; set => SetCurrency(value); }


        public PlayerUnitState(IPlayer player)
        {
            Player = player;
        }


        public void SetHealth(int value)
        {
            _health = Mathf.Clamp(value, 0, Stats.MaxHealth);
            PlayerEvents.InvokeHealthChanged(_health);
        }

        public void SetCurrency(int value)
        {
            _currency = value;
            PlayerEvents.InvokeInventoryChanged();
        }


        public void SetDefaultStats(PlayerStats value)
        {
            _playerStatsDefault = value;
            PlayerEvents.InvokeStatsChanged();
        }

        public void SetStats(PlayerStats value)
        {
            _playerStatsDelta += value;
            SetHealth(Health);
            PlayerEvents.InvokeStatsChanged();
        }


        public void ClearStatsDelta()
        {
            _playerStatsDelta = new PlayerStats();
            PlayerEvents.InvokeStatsChanged();
        }
    }
}