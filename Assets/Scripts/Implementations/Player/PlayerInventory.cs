using System;
using System.Collections.Generic;
using System.Linq;
using Game.Items;
using UnityEngine;

namespace Game.Player.Components
{
    public class PlayerInventory : PlayerComponent, IPlayerItems
    {
        [SerializeField] private Vector3 _dropOffset;
        [SerializeField] private Transform _weaponHandleTransform;

        private List<IItem> _equippedItems = new List<IItem>();

        [SerializeField] private int _currency;
        public int Currency { get => _currency; set => SetCurrency(value); }


        //Add physical storage here?
        public void PickItem(IItem item)
        {
            //Note: now items are compared by data, and i want to stack them
            //In theory there should not be cases when we equipping already taken item

            // if (_equippedItems.Contains(item))
            //     return;

            Debug.LogWarning("Picking up item");

            item.OnItemPicked(Player);
            PlaceItemForStorage(item);

            Player.Events.InvokeInventoryChanged();
        }


        public void DropItem(int itemID)
        {
            var item = _equippedItems.FirstOrDefault(t => t.ID == itemID);

            if (item != null)
                DropItem(item);
        }

        public void DropItem(IItem item)
        {
            if (!_equippedItems.Contains(item))
                return;

            Debug.LogWarning("Dropping item");
            _equippedItems.Remove(item);

            item.OnItemDropped(Player);
            PlaceItemForDrop(item);

            Player.Events.InvokeInventoryChanged();
        }


        public IEnumerable<IItem> GetItems()
        {
            return _equippedItems.AsEnumerable();
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


        private void SetCurrency(int value)
        {
            _currency = value;
            Player.Events.InvokeInventoryChanged();
        }


        private Vector3 GetDropPosition() => transform.position + Unit.State.Rotation * _dropOffset;
    }
}