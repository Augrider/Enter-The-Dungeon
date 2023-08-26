using Game.State;
using UnityEngine;

namespace Game.Enemies.Components
{
    public class Behavior_FollowPlayer : MovementBehavior
    {
        //TODO: Stopping distance should not be considered if no line of sight detected
        [SerializeField] private float _stoppingDistance;
        [SerializeField] protected LineOfSight _lineOfSight;


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

            if (_lineOfSight.CheckLineOfSight(PlayerPosition) && SqrDistance <= _stoppingDistance * _stoppingDistance)
                _movement.Stop();
            else
                _movement.SetDestination(PlayerPosition);
        }
    }
}