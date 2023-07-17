using System.Collections;
using System.Collections.Generic;
using Game.State;
using UnityEngine;

namespace Game.Map.Components
{
    public class LevelEnd : MonoBehaviour
    {
        public void ToNextLevel()
        {
            GameStateLocator.Service.SavePlayerState();
            GameStateLocator.Service.GoToLevel(GameStateLocator.Service.Level + 1);
        }
    }
}