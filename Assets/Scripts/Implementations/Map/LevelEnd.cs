using Game.Player;
using Game.Player.Components;
using Game.State;
using UnityEngine;

namespace Game.Map.Components
{
    public class LevelEnd : MonoBehaviour
    {
        public void ToNextLevel()
        {
            if (GameStateLocator.Service.Level >= GameStateLocator.Service.Control.LevelCount - 1)
            {
                //TODO: Proper ending
                PlayerEvents.InvokePlayerDied();
                return;
            }

            GameStateLocator.Service.Control.Save();
            GameStateLocator.Service.Control.GoToLevel(GameStateLocator.Service.Level + 1);
        }
    }
}