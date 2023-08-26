using Game.State;
using TMPro;
using UnityEngine;

namespace Game.UI.GameOverScreen
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private TextMeshProUGUI _enemiesKilledText;
        [SerializeField] private TextMeshProUGUI _levelReachedText;


        public void ShowScreen()
        {
            _canvas.enabled = true;

            _enemiesKilledText.SetText(GameStateLocator.Service.EnemiesKilled.ToString());
            _levelReachedText.SetText((GameStateLocator.Service.Level + 1).ToString());

            GameStateLocator.Service.Control.EraseSave();
        }


        public void Restart()
        {
            GameStateLocator.Service.Control.GoToLevel(0);
        }

        public void ToMainMenu()
        {
            GameStateLocator.Service.Control.ToMainMenu();
        }
    }
}