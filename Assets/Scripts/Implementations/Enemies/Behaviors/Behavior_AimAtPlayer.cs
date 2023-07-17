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
            if (IsPlayerAlive)
                _directionTransform.forward = (PlayerUnit.State.Position - transform.position).normalized;
        }
    }
}