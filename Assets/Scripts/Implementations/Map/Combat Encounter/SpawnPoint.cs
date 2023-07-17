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
        private IUnit _unit;

        [SerializeField] private UnityEvent _unitSpawned;
        [SerializeField] private UnityEvent _unitDied;

        public bool IsAlive => _unit != null ? _unit.State.Health > 0 : false;

        public event System.Action UnitSpawned;
        public event System.Action UnitDied;


        [ContextMenu("Spawn Unit")]
        public void SpawnUnit()
        {
            if (IsAlive)
                return;

            StartCoroutine(SpawnProcess());
        }



        private void OnUnitStateChanged()
        {
            if (IsAlive || _unit == null)
                return;

            _unit = null;

            UnitDied?.Invoke();
            _unitDied?.Invoke();
        }


        private IEnumerator SpawnProcess()
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = transform.position;

            yield return new WaitForSeconds(_spawnDelay);

            Destroy(sphere);

            _unit = ObjectPoolLocator.Service.GetUnit(_unitPrefab);

            _unit.State.Position = transform.position;
            _unit.State.Rotation = transform.rotation;

            UnitSpawned?.Invoke();
            _unitSpawned?.Invoke();

            _unit.StateChanged += OnUnitStateChanged;
        }
    }
}