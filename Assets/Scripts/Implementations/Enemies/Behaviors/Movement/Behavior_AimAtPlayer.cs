using Game.Player;
using Game.State;
using UnityEngine;

namespace Game.Enemies.Components
{
    public class Behavior_AimAtPlayer : EnemyBehavior
    {
        [SerializeField] private Transform _directionTransform;


        protected override void OnPlay()
        {
        }

        protected override void OnStop()
        {
        }


        void Update()
        {
            if (GameStateLocator.Service.Paused)
                return;

            if (IsPlayerAlive)
                _directionTransform.forward = (Players.Current.Position - transform.position).normalized;
        }
    }
}