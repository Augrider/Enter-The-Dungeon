using System;
using System.Collections;
using Game.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Units.Components
{
    public class UnitHealthComponent : UnitComponent, IDestructible
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _health;
        [SerializeField] private float _immunityCooldown;

        [SerializeField] private UnityEvent<int> _healthChanged;

        private bool _immune;

        public event Action<int> HealthChanged;

        public int Health { get => _health; set => SetHealth(value); }
        public int MaxHealth { get => _maxHealth; set => SetMaxHealth(value); }

        public bool Immune => _immune;


        void Awake()
        {
            Unit.Destructible = this;
        }


        public void SetHealth(int value)
        {
            if (_health == value)
                return;

            _health = Mathf.Clamp(value, 0, MaxHealth);
            HealthChanged?.Invoke(_health);
            _healthChanged?.Invoke(_health);

            if (_health <= 0)
            {
                Unit.gameObject.SetActive(false);
                return;
            }
        }

        public void SetMaxHealth(int value)
        {
            _maxHealth = value;
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
    }
}