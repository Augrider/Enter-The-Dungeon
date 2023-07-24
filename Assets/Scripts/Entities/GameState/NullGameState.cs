using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.State
{
    public class NullGameState : IGameState
    {
        public event Action<bool> PauseSet
        {
            add => throw new NotImplementedException();
            remove => Debug.LogWarning("Null Game State unsubscribe");
        }

        public bool Paused => false;

        public int Level => 0;
        public int LevelCount => 0;

        public IGameStateControl Control => throw new NotImplementedException();

        public int EnemiesKilled { get => 0; set => Debug.LogWarning("Null Game State add Enemies killed"); }
    }
}