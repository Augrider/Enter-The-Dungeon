using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Game.Items.Components
{
    //TODO: Change this later to XML file editor, and make database actually read items from file
    [CreateAssetMenu(menuName = "Items/Database")]
    public class ItemDatabaseSO : ScriptableObject
    {
        [SerializeField] private List<Item> _items;


        public int IndexOf(Item item)
        {
            return _items.IndexOf(item);
        }

        public Item GetPrefab(int index)
        {
            return _items[index];
        }
    }
}