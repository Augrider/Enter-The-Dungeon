using Game.Items;
using Game.Items.Components;
using Game.Projectiles.Components;
using Game.Units;
using Game.Units.Components;
using UnityEngine;

namespace Game.Plugins.ObjectPool
{
    public class NullObjectPool : IObjectPool
    {
        public Item GetItem(Item item)
        {
            throw new System.NotImplementedException();
        }

        public Projectile GetProjectile(Projectile projectile, Transform parent)
        {
            throw new System.NotImplementedException();
        }

        public Unit GetUnit(Unit unit)
        {
            throw new System.NotImplementedException();
        }
    }
}