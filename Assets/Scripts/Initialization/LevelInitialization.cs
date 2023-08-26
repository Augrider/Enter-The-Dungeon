using System.Collections;
using Game.Player.Components;
using UnityEngine;
using Game.State;
using Game.Player;
using Game.Map.Components;

namespace Game.Initialization
{
    public class LevelInitialization : MonoBehaviour
    {
        [SerializeField] private SpawnPoint _playerSpawnPoint;


        void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            StartCoroutine(InitializeProcess());
        }



        private IEnumerator InitializeProcess()
        {
            yield return null;

            //Note: First level should overwrite save too. Also, on death or new game save should be deleted first
            GameStateLocator.Service.Control.Save();

            yield return null;

            var unitPrefab = PlayerDatabase.GetUnitPrefab(Players.Current.CharacterID);
            _playerSpawnPoint.SetPrefab(unitPrefab);
            _playerSpawnPoint.SpawnUnit();

            yield return null;

            //TODO: Create player here
            // Players.Current.InputEnabled = true;
            GameStateLocator.Service.Control.TogglePause(false);
        }
    }
}