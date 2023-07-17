using System.Collections;
using Game.Player.Components;
using UnityEngine;
using Game.SaveSystem;
using Game.State;

namespace Game.Initialization
{
    public class LevelInitialization : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private GameObject _UiObject;


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
            GameStateLocator.Service.LoadPlayerState();
            GameStateLocator.Service.SavePlayerState();

            yield return null;

            _playerInput.enabled = true;
            _UiObject.SetActive(true);
        }
    }
}