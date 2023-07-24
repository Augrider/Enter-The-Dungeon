using Game.Items;
using Game.Items.Components;
using Game.Projectiles.Components;
using Game.Units;
using Game.Units.Components;
using UnityEngine;

namespace Game.Plugins.ObjectPool
{
    public class ObjectPoolController : MonoBehaviour, IObjectPool
    {
        [SerializeField] private UnitPool _unitPool;
        [SerializeField] private ProjectilePool _projectilePool;
        [SerializeField] private ItemsPool _itemsPool;


        private void Awake()
        {
            ObjectPoolLocator.Provide(this);
        }

        private void OnDestroy()
        {
            ObjectPoolLocator.Provide(null);
        }


        public Item GetItem(Item item)
        {
            return _itemsPool.GetNew(item);
        }

        public Projectile GetProjectile(Projectile projectile, Transform parent)
        {
            var result = _projectilePool.GetNew(projectile);

            if (parent != null)
                result.transform.parent = parent;

            return result;
        }

        public Unit GetUnit(Unit unit)
        {
            return _unitPool.GetNew(unit);
        }
    }
}