using System;
using System.Collections;
using System.Collections.Generic;
using Game.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Player.Components
{
    public class PlayerUnitHealthComponent : PlayerUnitComponent, IDestructible
    {
        [SerializeField] private float _immunityCooldown;

        [SerializeField] private UnityEvent<int> _healthChanged;

        private bool _immune;

        public event Action<int> HealthChanged;

        public int Health { get => PlayerState.Health; set => SetHealth(value); }
        public int MaxHealth { get => PlayerUnit.Stats.MaxHealth; set => SetMaxHealth(value); }

        public bool Immune => _immune;


        void Awake()
        {
            PlayerUnit.Destructible = this;
            PlayerEvents.HealthChanged += Notify;
            //Subscribe to health event
        }

        void OnDestroy()
        {
            PlayerEvents.HealthChanged -= Notify;
        }


        public void SetHealth(int value)
        {
            // if (PlayerState.Health == value)
            //     return;

            PlayerState.Health = Mathf.Clamp(value, 0, MaxHealth);

            Notify(PlayerState.Health);

            if (PlayerState.Health <= 0)
            {
                PlayerUnit.gameObject.SetActive(false);
                return;
            }
        }

        public void SetMaxHealth(int value)
        {
            var delta = new PlayerStats() { MaxHealth = value - PlayerUnit.Stats.MaxHealth };
            PlayerUnit.Stats += delta;

            SetHealth(Health);
        }


        public void ReceiveDamage(int value)
        {
            if (value == 0)
                return;

            SetHealth(Health - value);

            if (Health > 0 && _immunityCooldown > 0)
                StartCoroutine(ImmunityProcess());
        }



        private IEnumerator ImmunityProcess()
        {
            _immune = true;

            yield return new WaitForSeconds(_immunityCooldown);

            _immune = false;
        }


        private void Notify(int health)
        {
            HealthChanged?.Invoke(health);
            _healthChanged?.Invoke(health);
        }
    }
}