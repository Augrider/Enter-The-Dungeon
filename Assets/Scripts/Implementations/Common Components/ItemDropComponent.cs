using Game.Player;
using Game.Plugins.ObjectPool;
using UnityEngine;

namespace Game.Items.Components
{
    public class ItemDropComponent : MonoBehaviour
    {
        [SerializeField] private ItemTable _items;
        [SerializeField] private float _offsetRange;


        public void DropCurrency(int currency)
        {
            Players.Current.Inventory.Currency += currency;
        }


        public void DropRandomItem(float probability)
        {
            float roll = Random.Range(0, 1f);

            if (roll <= probability)
            {
                var position = GetDropPosition(transform.position);
                CreateItem(_items.GetRandomPrefab(), position, Quaternion.identity);
            }
        }

        public void DropRandomItems(int maxAmount)
        {
            int amount = Random.Range(0, maxAmount);

            for (int i = 0; i < amount; i++)
            {
                var position = GetDropPosition(transform.position);
                CreateItem(_items.GetRandomPrefab(), position, Quaternion.identity);
            }
        }

        [ContextMenu("Drop all Items")]
        public void DropAllItems()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                var position = GetDropPosition(transform.position);
                CreateItem(_items.GetPrefab(i), position, Quaternion.identity);
            }
        }



        private void CreateItem(Item prefab, Vector3 position, Quaternion rotation)
        {
            var item = ObjectPoolLocator.Service.GetItem(prefab);
            item.SetPosition(position, rotation);
        }

        private Vector3 GetDropPosition(Vector3 position)
        {
            var direction = Random.insideUnitCircle;

            return position + _offsetRange * new Vector3(direction.x, 0, direction.y) + Vector3.up;
        }
    }
}