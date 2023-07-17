using UnityEngine;

namespace Game.Items.Components
{
    [System.Serializable]
    public struct ItemTable
    {
        [SerializeField] private Item[] _items;

        public int Count => _items.Length;


        /// <summary>
        /// Get item prefab from table, selected randomly without weights
        /// </summary>
        /// <returns>Item prefab reference</returns>
        public Item GetRandomPrefab()
        {
            return _items[Random.Range(0, Count)];
        }

        /// <summary>
        /// Get item prefab from table, selected randomly without weights
        /// </summary>
        /// <returns>Item prefab reference</returns>
        public Item GetPrefab(int index)
        {
            return _items[index];
        }

        //TODO: Get Weighted Prefab based on item rarity
    }
}