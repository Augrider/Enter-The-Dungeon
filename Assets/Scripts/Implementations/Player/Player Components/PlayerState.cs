using Game.Units;
using UnityEngine;

namespace Game.Player.Components
{
    public class PlayerState : PlayerComponent, IPlayerState
    {
        private PlayerStats _playerStatsDefault;
        private PlayerStats _playerStatsDelta;

        private int _health;
        private int _currency = 0;

        public PlayerStats Stats
        {
            get => _playerStatsDefault + _playerStatsDelta;
            set => SetStatsDelta(value - _playerStatsDefault);
        }

        public int Health { get => _health; set => SetHealth(value); }
        public int Currency { get => _currency; set => SetCurrency(value); }


        public PlayerState(Player player) : base(player)
        {
        }


        public void SetHealth(int value)
        {
            _health = Mathf.Clamp(value, 0, Stats.MaxHealth);
            PlayerEvents.InvokeHealthChanged(Health);
        }

        public void SetCurrency(int value)
        {
            _currency = value;
            PlayerEvents.InvokeInventoryChanged();
        }


        public void SetDefaultStats(PlayerStats value)
        {
            _playerStatsDefault = value;
        }

        public void SetStatsDelta(PlayerStats value)
        {
            _playerStatsDelta += value;
            SetHealth(Health);
        }


        public void ClearStatsDelta()
        {
            _playerStatsDelta = new PlayerStats();
        }
    }
}