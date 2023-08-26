using System;
using UnityEngine;

namespace Game.Units.Components
{
    public class UnitState : UnitComponent, IUnitState
    {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;

        public int Health { get => _health; set => SetHealth(value); }
        public int MaxHealth { get => _maxHealth; set => SetMaxHealth(value); }


        void Awake()
        {
            Health = MaxHealth;
        }


        public void SetHealth(int value)
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);
            // Unit.InvokeStateChanged();
        }

        public void SetMaxHealth(int value)
        {
            _maxHealth = value;
            _health = Mathf.Clamp(_health, 0, MaxHealth);

            // Unit.InvokeStateChanged();
        }


        // public void ApplyStatsChange(UnitStats stats)
        // {
        //     _stats += stats;

        //     Health = Mathf.Clamp(Health, 0, _stats.MaxHealth);

        //     Unit.InvokeStateChanged();
        // }
    }
}