using System.Collections.Generic;
using Game.Items;
using UnityEngine;

namespace Game.Player.Components
{
    //TODO: Change strategy: Inventory will keep all item ID, weapon Data (maybe) and other inventory, while most logic will be delegated to player unit
    //In that case we still can have data permanence, while logic can be controlled right inside unit and it's mono behaviors
    public class PlayerInventory : PlayerComponent, IPlayerItems
    {
        /// <summary>
        /// List of id of all non consumable items picked up by player
        /// </summary>
        private List<string> _items = new List<string>();


        public PlayerInventory(Player player) : base(player)
        {
        }


        public void PickItem(IItem item)
        {
            if (item.Data.ItemType != ItemType.Consumable)
                _items.Add(item.ID);

            item.Destroy();
            PlayerEvents.InvokeInventoryChanged();
        }

        public void DropItem(string itemID)
        {
            if (!_items.Contains(itemID))
                return;

            _items.Remove(itemID);
            PlayerEvents.InvokeInventoryChanged();
        }


        internal string[] Export()
        {
            Debug.Log($"Player Inventory: Exported {_items.Count} items");

            return _items.ToArray();
        }

        internal void Import(PlayerStateData data)
        {
            _items.Clear();
            _items.AddRange(data.Items);

            Debug.Log($"Player Inventory: Imported {_items.Count} items");
        }
    }
}