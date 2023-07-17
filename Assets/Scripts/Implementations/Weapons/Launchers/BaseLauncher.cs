using Game.Plugins.ObjectPool;
using Game.Projectiles.Components;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Weapons.Components
{
    public abstract class BaseLauncher : MonoBehaviour, IProjectileLauncher
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private bool _attached;

        [SerializeField] private UnityEvent _shotFired;

        public void Shoot(float deviation)
        {
            OnShoot(deviation);
            _shotFired?.Invoke();
        }


        protected abstract void OnShoot(float deviation);


        protected void CreateProjectile(Vector3 position, Vector3 direction)
        {
            var projectile = ObjectPoolLocator.Service.GetProjectile(_projectilePrefab, _attached ? transform : null);

            projectile.transform.position = position;

            projectile.transform.rotation = Quaternion.identity;
            projectile.transform.forward = direction;

            projectile.SetProjectileState(ProjectileState.Active);
        }


        protected static Vector3 GetDeviatedDirection(Vector3 direction, float deviationAngle)
        {
            var result = direction;

            float sin = Mathf.Sin(deviationAngle * Mathf.Deg2Rad);
            float cos = Mathf.Cos(deviationAngle * Mathf.Deg2Rad);

            result.x = (cos * direction.x) + (sin * direction.z);
            result.z = -(sin * direction.x) + (cos * direction.z);

            return result.normalized;
        }
    }
}