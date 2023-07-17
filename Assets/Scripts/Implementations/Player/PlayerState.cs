using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.Components
{
    public class PlayerState : PlayerComponent, IPlayerState
    {
        [SerializeField] private PlayerStats _playerStatsDefault;
        private PlayerStats _playerStatsDelta;

        [SerializeField] private int _health;

        public PlayerStats Stats
        {
            get => _playerStatsDefault + _playerStatsDelta;
            set => SetStats(value - _playerStatsDefault);
        }

        public int Health { get => _health; set => SetHealth(value); }


        public void SetHealth(int value)
        {
            _health = Mathf.Clamp(value, 0, Stats.MaxHealth);
            Unit.State.Health = Health;

            Player.Events.InvokeHealthChanged(_health);
        }

        public void SetStats(PlayerStats value)
        {
            _playerStatsDelta += value;
            SetHealth(Health);
            // Player.Events.InvokeStatsChanged(Stats);
        }
    }
}