using System;
using UnityEngine;

namespace Game.State.Components
{
    public class GameStateComponent : MonoBehaviour, IGameState
    {
        public event Action<bool> PauseSet;

        [SerializeField] private GameStateController _gameStateController;

        public bool Paused { get; internal set; }

        public int Level { get; internal set; }
        public int EnemiesKilled { get; set; }

        public IGameStateControl Control => _gameStateController;


        private void Awake()
        {
            GameStateLocator.Provide(this);
        }

        private void OnDestroy()
        {
            GameStateLocator.Provide(null);
        }


        internal void InvokePauseSet(bool value) => PauseSet?.Invoke(value);
    }
}