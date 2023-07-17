using Game.Items;
using Game.Items.Components;
using Game.Projectiles.Components;
using Game.Units;
using Game.Units.Components;
using UnityEngine;

namespace Game.Plugins.ObjectPool
{
    public interface IObjectPool
    {
        IItem GetItem(Item item);
        Projectile GetProjectile(Projectile projectile, Transform parent);
        IUnit GetUnit(Unit unit);
    }
}