using Game.Plugins.TimeProcesses;
using UnityEngine;

namespace Game.Enemies.Components
{
    public class Behavior_Wander : MovementBehavior
    {
        [SerializeField] protected LineOfSight _lineOfSight;

        [SerializeField] private float _newPointCooldown;
        [SerializeField] private float _pointMaxDistance;

        private Vector3 _previousPoint;
        private ITimeProcess _movementProcess;


        protected override void OnPlay()
        {
            SetDestination();
        }

        protected override void OnStop()
        {
            _movement.Stop();

            _movementProcess?.Stop();
            // _movementProcess = null;
        }



        private void SetDestination()
        {
            Vector3 destination = GetWanderPoint();

            _movement.SetDestination(destination);
            _previousPoint = transform.position;

            _movementProcess = TimeProcessLocator.Service.DoAfterDelay(SetDestination, _newPointCooldown);
        }

        private Vector3 GetWanderPoint()
        {
            var delta = Random.insideUnitCircle.normalized * _pointMaxDistance;
            var destination = transform.position + new Vector3(delta.x, 0, delta.y);

            if (_lineOfSight.CheckLineOfSight(destination))
                return destination;

            return _previousPoint;
        }
    }
}