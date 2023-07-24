using Game.State;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class MainMenuComponent : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;


        private void Start()
        {
            _resumeButton.interactable = GameStateLocator.Service.Control.IsPlayerSaved();
        }


        public void Resume()
        {
            GameStateLocator.Service.Control.LoadPlayerState(true);
        }

        public void StartNew()
        {
            GameStateLocator.Service.Control.ErasePlayerSave();
            GameStateLocator.Service.Control.GoToLevel(0);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}