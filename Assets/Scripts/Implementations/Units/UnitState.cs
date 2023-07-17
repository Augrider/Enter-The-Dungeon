using System;
using UnityEngine;

namespace Game.Units.Components
{
    public class UnitState : UnitComponent, IUnitState
    {
        [SerializeField] private Transform _turnTransform;

        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;

        public Vector3 Position { get => transform.position; set => SetPosition(value); }

        public Quaternion Rotation { get => _turnTransform.rotation; set => SetRotation(value); }
        public Vector3 Direction { get => _turnTransform.forward; set => SetDirection(value); }

        public int Health { get => _health; set => SetHealth(value); }
        public int MaxHealth { get => _maxHealth; set => SetMaxHealth(value); }


        void Awake()
        {
            Health = MaxHealth;
        }


        public void SetPosition(Vector3 value)
        {
            transform.position = value;
        }

        public void SetRotation(Quaternion value)
        {
            _turnTransform.rotation = value;
        }

        public void SetDirection(Vector3 value)
        {
            var direction2d = value;
            direction2d.y = 0;

            _turnTransform.forward = direction2d;
        }


        public void SetHealth(int value)
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);
            Unit.InvokeStateChanged();
        }

        public void SetMaxHealth(int value)
        {
            _maxHealth = value;
            _health = Mathf.Clamp(_health, 0, MaxHealth);

            Unit.InvokeStateChanged();
        }


        // public void ApplyStatsChange(UnitStats stats)
        // {
        //     _stats += stats;

        //     Health = Mathf.Clamp(Health, 0, _stats.MaxHealth);

        //     Unit.InvokeStateChanged();
        // }
    }
}