using System.Collections.Generic;
using System.Linq;
using Game.Items;
using Game.Items.Components;
using Game.Plugins.ObjectPool;
using UnityEngine;

namespace Game.Player.Components
{
    //TODO: How to make this independent from Player, but not make things worse?
    //TODO: If items interact with player, how to do it without it?
    //TODO: Where to call event?
    public class ItemsInventory : PlayerUnitComponent, IPlayerItems
    {
        [SerializeField] private Vector3 _dropOffset;
        [SerializeField] private Transform _weaponHandleTransform;

        private List<IItem> _equippedItems = new List<IItem>();


        public void PickItem(IItem item)
        {
            //Note: In theory there should not be cases when player is equipping already taken item
            item.OnItemPicked(PlayerUnit);
            PlaceItemForStorage(item);
        }

        public void DropItem(string itemID)
        {
            var item = _equippedItems.First(t => t.ID == itemID);

            Debug.LogWarning("Dropping item");
            _equippedItems.Remove(item);

            item.OnItemDropped(PlayerUnit);
            PlaceItemForDrop(item);
            // return item;
        }


        internal void Clear()
        {
            foreach (var item in _equippedItems)
            {
                item.OnItemDropped(PlayerUnit);
                PlaceItemForDrop(item);

                item.Destroy();
            }

            _equippedItems.Clear();
        }


        internal string[] Export()
        {
            return _equippedItems.Select(t => t.ID).ToArray();
        }

        internal void Import(PlayerStateData data)
        {
            Clear();

            Debug.Log($"Player Unit Inventory: Importing {data.Items.Length} items");

            foreach (var id in data.Items)
            {
                IItem item = ObjectPoolLocator.Service.GetItem(ItemDatabase.GetPrefab(id));

                Debug.Log($"Player Unit Inventory: Picking up item {item}");
                PickItem(item);
                // item.OnItemPicked(Player);
                // ItemsInventory.PickItem(item);
            }

            Debug.Log($"Player Unit Inventory: Imported {_equippedItems.Count} items");
        }



        private void PlaceItemForStorage(IItem item)
        {
            switch (item.Data.ItemType)
            {
                case ItemType.Consumable:
                    item.Destroy();
                    return;

                default:
                    PlaceInSlot(item);
                    _equippedItems.Add(item);
                    return;
            }
        }

        private void PlaceInSlot(IItem item)
        {
            switch (item.Data.ItemSlot)
            {
                default:
                    item.SetPosition(transform, transform.position, transform.rotation);
                    return;

                case ItemSlot.Weapon:
                    item.SetPosition(_weaponHandleTransform, _weaponHandleTransform.position, _weaponHandleTransform.rotation);
                    return;
            }
        }


        private void PlaceItemForDrop(IItem item)
        {
            Debug.LogWarning("Dropping!");
            item.SetPosition(GetDropPosition(), transform.rotation);
        }


        private Vector3 GetDropPosition() => transform.position + PlayerUnit.Transform.Rotation * _dropOffset;
    }
}