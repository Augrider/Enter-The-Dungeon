using System;
using Game.Player;
using Game.Player.Components;
using Game.State;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI.HUD
{
    /// <summary>
    /// Handles interactions between game and UI elements
    /// </summary>
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private UnityEvent<IPlayer> PlayerSpawned;
        [SerializeField] private UnityEvent<IPlayer> PlayerDied;


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



        private void OnPlayerSpawned(IPlayer player)
        {
            Debug.LogWarning("Detected spawn!");
            PlayerSpawned?.Invoke(player);
        }

        private void OnPlayerDied(IPlayer player)
        {
            PlayerDied?.Invoke(player);
        }
    }
}