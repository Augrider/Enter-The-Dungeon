using Game.Projectiles.Components;

namespace Game.Plugins.ObjectPool
{
    public class ProjectilePool : ObjectPool<Projectile>
    {
        protected override void OnNewActivated(Projectile result)
        {
            result.transform.parent = transform;
        }
    }
}