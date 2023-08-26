using Game.Player.Components;
using Game.State;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class MainMenuComponent : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;
        //TODO: Character selection screen
        [SerializeField] private PlayerData _selectedCharacter;


        private void Start()
        {
            _resumeButton.interactable = GameStateLocator.Service.Control.IsSaved();
        }


        public void Resume()
        {
            GameStateLocator.Service.Control.LoadSave();
        }

        public void StartNew()
        {
            GameStateLocator.Service.Control.EraseSave();
            //Load selected character
            //Player Data is fairly good choice for it
            //But using ID components (or it's interface) will make code less dependant on implementations

            GameStateLocator.Service.Control.LoadPlayer(_selectedCharacter.GetFullData());
            GameStateLocator.Service.Control.GoToLevel(0);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}