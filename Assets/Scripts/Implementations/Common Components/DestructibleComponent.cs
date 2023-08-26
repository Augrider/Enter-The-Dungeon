using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Common
{
    public class DestructibleComponent : MonoBehaviour, IDestructible
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _health;
        [SerializeField] private float _immunityCooldown;

        [SerializeField] private UnityEvent<int> _healthChanged;
        [SerializeField] private UnityEvent<int> _receivedDamage;
        [SerializeField] private UnityEvent _healthAtZero;

        private bool _immune;

        public event Action<int> HealthChanged;

        public int Health { get => _health; set => SetHealth(value); }
        public int MaxHealth { get => _maxHealth; set => SetMaxHealth(value); }

        public bool Immune => _immune;


        public void SetHealth(int value)
        {
            // if (_health == value)
            //     return;

            _health = Mathf.Clamp(value, 0, MaxHealth);
            HealthChanged?.Invoke(_health);
            _healthChanged?.Invoke(_health);

            if (_health <= 0)
            {
                _healthAtZero?.Invoke();
                return;
            }
        }

        public void SetMaxHealth(int value)
        {
            _maxHealth = value;
            SetHealth(Health);
        }

        public void ResetHealth() => SetHealth(MaxHealth);


        public void ReceiveDamage(int value)
        {
            SetHealth(Health - value);
            _receivedDamage?.Invoke(value);

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