using Game.State;
using UnityEngine;

namespace Game.Enemies.Components
{
    public class Behavior_FollowPlayer : MovementBehavior
    {
        [SerializeField] private float _stoppingDistance;


        protected override void OnPlay()
        {
        }

        protected override void OnStop()
        {
            _movement.Stop();
        }


        void Update()
        {
            if (GameStateLocator.Service.Paused)
                return;

            if (!IsPlayerAlive)
                return;

            if (SqrDistance > _stoppingDistance * _stoppingDistance)
                _movement.SetDestination(PlayerPosition);
            else
                _movement.Stop();
        }
    }
}