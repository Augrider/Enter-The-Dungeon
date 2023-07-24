using Game.Common;
using Game.Plugins.TimeProcesses;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Units.Components
{
    public class UnitHealthComponent : UnitComponent, IDestructible
    {
        [SerializeField] private float _immunityCooldown;

        [SerializeField] private UnityEvent UnitSpawned;
        [SerializeField] private UnityEvent<int> HealthChanged;
        [SerializeField] private UnityEvent UnitDied;

        private int _health;
        private bool _immune;

        public bool Immune => _immune;


        void OnEnable()
        {
            Unit.StateChanged += OnStateChanged;
            _health = Unit.State.Health;

            UnitSpawned?.Invoke();
        }

        void OnDisable()
        {
            Unit.StateChanged -= OnStateChanged;
        }


        public void DealDamage(int value)
        {
            Unit.State.Health -= value;
            _health = Unit.State.Health;

            if (Unit.State.Health <= 0)
            {
                UnitDied?.Invoke();
                Unit.gameObject.SetActive(false);
                return;
            }

            if (_immunityCooldown > 0)
            {
                _immune = true;
                TimeProcessLocator.Service.DoAfterDelay(() => _immune = false, _immunityCooldown);
            }
        }



        //Health can be increased or changed by other means
        private void OnStateChanged()
        {
            if (_health == Unit.State.Health)
                return;

            HealthChanged?.Invoke(Unit.State.Health);
        }
    }
}