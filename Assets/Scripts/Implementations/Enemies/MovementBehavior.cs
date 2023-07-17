using Game.Player;
using Game.Units.Components;
using UnityEngine;

namespace Game.Enemies.Components
{
    public abstract class MovementBehavior : EnemyBehavior
    {
        [SerializeField] protected NavMeshMovementComponent _movement;

        protected Vector3 PlayerPosition => Players.Current.UnitState.Position;
        protected float SqrDistance => (PlayerPosition - transform.position).sqrMagnitude;
    }
}