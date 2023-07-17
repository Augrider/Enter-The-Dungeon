using System;
using UnityEngine;

namespace Game.Units.Components
{
    public class Unit : MonoBehaviour, IUnit
    {
        public event Action StateChanged;

        [SerializeField] private UnitState _state;
        public IUnitState State => _state;


        internal void InvokeStateChanged() => StateChanged?.Invoke();
    }
}