using Game.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI.HUD
{
    /// <summary>
    /// Handles interactions between game and UI elements
    /// </summary>
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Canvas _pauseMenuCanvas;
        [SerializeField] private Canvas _hudCanvas;

        [SerializeField] private UnityEvent<IPlayer> _playerSpawned;
        [SerializeField] private UnityEvent<IPlayer> _playerDied;

        [SerializeField] private UnityEvent<bool> _pauseToggled;

        public bool PauseControlLocked { get; set; } = false;


        private void OnEnable()
        {
            PlayerEvents.PlayerSpawned += OnPlayerSpawned;
            PlayerEvents.PlayerDied += OnPlayerDied;
        }

        private void OnDisable()
        {
            PlayerEvents.PlayerSpawned -= OnPlayerSpawned;
            PlayerEvents.PlayerDied -= OnPlayerDied;
        }


        public void SwitchPauseMenu()
        {
            //Control pause menu and canvas
            //Switch pause state
            //Do nothing if locked somehow
            if (PauseControlLocked)
                return;

            _hudCanvas.enabled = !_hudCanvas.enabled;
            _pauseMenuCanvas.enabled = !_pauseMenuCanvas.enabled;

            _pauseToggled?.Invoke(_pauseMenuCanvas.enabled);
        }



        private void OnPlayerSpawned()
        {
            Debug.LogWarning("Detected spawn!");
            _playerSpawned?.Invoke(Players.Current);
        }

        private void OnPlayerDied()
        {
            _playerDied?.Invoke(Players.Current);
        }
    }
}