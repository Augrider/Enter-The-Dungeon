using System;
using Game.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Units.Components
{
    public class Unit : MonoBehaviour, IUnit
    {
        [SerializeField] private TransformComponent _transformComponent;

        [Space]
        [Header("Unit life events")]
        [Space]
        [SerializeField] private UnityEvent UnitSpawned;
        [SerializeField] private UnityEvent UnitDied;

        // [SerializeField] private UnitState _state;

        public ITransformComponent Transform => _transformComponent;
        public IDestructible Destructible { get; set; }

        // public IUnitState State => _state;


        void OnEnable()
        {
            UnitSpawned?.Invoke();

            Destructible.Health = Destructible.MaxHealth;
        }

        void OnDisable()
        {
            UnitDied?.Invoke();
        }
    }
}