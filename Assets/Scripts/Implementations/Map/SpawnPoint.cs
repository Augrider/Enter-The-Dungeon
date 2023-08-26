using System.Collections;
using Game.Plugins.ObjectPool;
using Game.Units;
using Game.Units.Components;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Map.Components
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Unit _unitPrefab;
        [SerializeField] private float _spawnDelay;
        private Unit _unit;

        [SerializeField] private UnityEvent<Unit> _unitSpawned;
        [SerializeField] private UnityEvent<Unit> _unitDied;

        public bool IsAlive => _unit != null ? _unit.Destructible.Health > 0 : false;

        public event System.Action UnitSpawned;
        public event System.Action UnitDied;


        public void SetPrefab(Unit unit)
        {
            _unitPrefab = unit;
        }

        [ContextMenu("Spawn Unit")]
        public void SpawnUnit()
        {
            if (IsAlive)
                return;

            StartCoroutine(SpawnProcess());
        }



        private void OnUnitHealthChanged(int health)
        {
            if (health > 0)
                return;

            OnUnitDied();
        }

        private void OnUnitDied()
        {
            _unit.Destructible.HealthChanged -= OnUnitHealthChanged;
            _unit = null;

            UnitDied?.Invoke();
            _unitDied?.Invoke(_unit);
        }

        private IEnumerator SpawnProcess()
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = transform.position;

            yield return new WaitForSeconds(_spawnDelay);

            Destroy(sphere);

            _unit = ObjectPoolLocator.Service.GetUnit(_unitPrefab);

            _unit.Transform.Position = transform.position;
            _unit.Transform.Rotation = transform.rotation;

            UnitSpawned?.Invoke();
            _unitSpawned?.Invoke(_unit);

            _unit.Destructible.HealthChanged += OnUnitHealthChanged;
        }
    }
}