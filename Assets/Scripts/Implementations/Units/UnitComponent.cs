using UnityEngine;

namespace Game.Units.Components
{
    public abstract class UnitComponent : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        public Unit Unit => _unit;
    }
}