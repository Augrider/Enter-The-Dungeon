using Game.State;
using UnityEngine;

namespace Game.UI.PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        public void Resume()
        {
            GameStateLocator.Service.Control.TogglePause(false);
        }

        public void Restart()
        {
            GameStateLocator.Service.Control.ErasePlayerSave();
            GameStateLocator.Service.Control.GoToLevel(0);
        }

        public void ToMainMenu()
        {
            GameStateLocator.Service.Control.ToMainMenu();
        }
    }
}