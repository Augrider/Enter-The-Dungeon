using Game.Common;
using Game.Player;
using Game.Units.Components;
using UnityEngine;

namespace Game.Enemies.Components
{
    public abstract class MovementBehavior : EnemyBehavior
    {
        [SerializeField] protected TransformComponent _transform;
        [SerializeField] protected NavMeshMovementComponent _movement;

        protected Vector3 PlayerPosition => Players.Current.Position;
        protected float SqrDistance => (PlayerPosition - _transform.Position).sqrMagnitude;
    }
}