using Game.State;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI.PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private UnityEvent _resumeButtonPressed;


        public void Resume()
        {
            _resumeButtonPressed?.Invoke();
        }

        public void Restart()
        {
            GameStateLocator.Service.Control.EraseSave();
            GameStateLocator.Service.Control.GoToLevel(0);
        }

        public void ToMainMenu()
        {
            GameStateLocator.Service.Control.ToMainMenu();
        }
    }
}