using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Map.Components
{
    /// <summary>
    /// Component used for spawning enemies in the room and locking it up
    /// </summary>
    public class CombatEncounter : MonoBehaviour
    {
        [SerializeField] private Door[] _doors;
        [SerializeField] private WaveComposition[] _waves;

        [SerializeField] private float _encounterDelay;

        [SerializeField] private UnityEvent _encounterFinished;

        private int _currentWave;
        private int _currentAlive;

        public int WaveCount => _waves.Length;


        public void StartEncounter()
        {
            _currentWave = 0;

            StartCoroutine(WaitAndStart());
        }



        private IEnumerator WaitAndStart()
        {
            yield return new WaitForSeconds(_encounterDelay);

            SpawnWave(_waves[_currentWave]);
            ToggleDoorsLocked(true);
        }


        private void OnWaveFinished()
        {
            foreach (var point in _waves[_currentWave].SpawnPoints)
            {
                point.UnitDied -= OnUnitDied;
            }

            _currentWave++;

            if (_currentWave >= WaveCount)
                OnEncounterFinished();
            else
                SpawnWave(_waves[_currentWave]);
        }

        private void OnEncounterFinished()
        {
            ToggleDoorsLocked(false);
            _encounterFinished?.Invoke();
        }


        private void OnUnitDied()
        {
            _currentAlive--;

            if (_currentAlive <= 0)
                OnWaveFinished();
        }


        private void ToggleDoorsLocked(bool value)
        {
            foreach (var door in _doors)
                door.ToggleLocked(value);
        }

        private void SpawnWave(WaveComposition waveComposition)
        {
            foreach (var point in waveComposition.SpawnPoints)
            {
                _currentAlive++;

                point.SpawnUnit();
                point.UnitDied += OnUnitDied;
            }
        }



        [System.Serializable]
        private struct WaveComposition
        {
            public SpawnPoint[] SpawnPoints;
        }
    }
}