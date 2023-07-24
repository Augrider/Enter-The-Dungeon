using UnityEngine;

namespace Game.State.Components
{
    public class OnEnemyKilledComponent : MonoBehaviour
    {
        public void OnKilled()
        {
            GameStateLocator.Service.EnemiesKilled++;
        }
    }
}