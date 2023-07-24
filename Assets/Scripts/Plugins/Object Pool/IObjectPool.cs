using Game.Items.Components;
using Game.Projectiles.Components;
using Game.Units.Components;
using UnityEngine;

namespace Game.Plugins.ObjectPool
{
    public interface IObjectPool
    {
        Item GetItem(Item item);
        Projectile GetProjectile(Projectile projectile, Transform parent);
        Unit GetUnit(Unit unit);
    }
}