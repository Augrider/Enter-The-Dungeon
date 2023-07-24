using System.Collections;
using Game.Player.Components;
using UnityEngine;
using Game.State;

namespace Game.Initialization
{
    public class LevelInitialization : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;


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
            GameStateLocator.Service.Control.LoadPlayerState();
            GameStateLocator.Service.Control.SavePlayerState();

            yield return null;

            _playerInput.enabled = true;
        }
    }
}