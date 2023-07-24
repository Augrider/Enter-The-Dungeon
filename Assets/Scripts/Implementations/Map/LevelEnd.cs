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
            if (GameStateLocator.Service.Level + 1 >= GameStateLocator.Service.Control.LevelCount)
            {
                //TODO: Proper ending
                PlayerEvents.InvokePlayerDied(Players.Current);
                return;
            }

            GameStateLocator.Service.Control.SavePlayerState();
            GameStateLocator.Service.Control.GoToLevel(GameStateLocator.Service.Level + 1);
        }
    }
}