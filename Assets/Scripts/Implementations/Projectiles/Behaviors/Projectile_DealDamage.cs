using Game.Common;
using UnityEngine;

namespace Game.Projectiles.Components
{
    public class Projectile_DealDamage : ProjectileComponent
    {
        [SerializeField] private int _damage;

        [SerializeField] private bool _pierce;
        [SerializeField] private bool _pierceThroughWalls;


        void OnTriggerEnter(Collider other)
        {
            if (Projectile.ProjectileState != ProjectileState.Active)
                return;

            Debug.Log($"Projectile {this} collided with {other.gameObject}");

            if (other.TryGetComponent<IDestructible>(out var component) && !component.Immune)
            {
                component.ReceiveDamage(_damage);

                if (!_pierce)
                    Projectile.SetProjectileState(ProjectileState.Destroyed);

                return;
            }

            if (!_pierceThroughWalls)
                Projectile.SetProjectileState(ProjectileState.Destroyed);
        }
    }
}