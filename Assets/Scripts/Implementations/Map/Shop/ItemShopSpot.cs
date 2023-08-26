using Game.Common;
using Game.Items;
using Game.Items.Components;
using Game.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Map.Components
{
    public class ItemShopSpot : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _itemPlacement;
        [SerializeField] private Vector3 _dropOffset;

        [SerializeField] private TextMeshProUGUI _priceTextObject;

        [SerializeField] private UnityEvent<bool> HighlightToggled;

        private Item _current;
        private int _price;

        public bool ItemPlaced { get; private set; }
        public IItem Item { get => _current; }

        public bool Enabled { get => enabled; set => ToggleEnabled(value); }
        public Vector3 Position => transform.position;


        void Start()
        {
            Debug.Log($"Enabled {Enabled}");
        }


        public void Interact(IPlayer player)
        {
            if (player.State.Currency < _price)
                return;

            player.State.Currency -= _price;
            _current.Interact(player);
            // player.Interactions.TryInteractWith(_current);

            _current = null;
            ItemPlaced = false;

            ToggleEnabled(false);
        }

        public void ToggleHighlight(bool value)
        {
            HighlightToggled?.Invoke(value);
        }


        internal void PlaceItem(IItem item, int price)
        {
            if (ItemPlaced)
                DropItem();

            ItemPlaced = true;
            _current = item as Item;

            _current.Enabled = false;
            enabled = true;

            _current.SetPosition(_itemPlacement, _itemPlacement.position, _itemPlacement.rotation);
            _current.TogglePhysics(false);

            SetPriceTag(price);
        }

        internal void DropItem()
        {
            _current.Enabled = true;
            enabled = false;

            _current.SetPosition(_itemPlacement.position + _dropOffset, _itemPlacement.rotation);
            _current.TogglePhysics(true);

            ItemPlaced = false;
        }



        private void ToggleEnabled(bool value)
        {
            enabled = value;
            _priceTextObject.enabled = value;

            ToggleHighlight(false);

            if (!value && ItemPlaced)
                DropItem();
        }

        private void SetPriceTag(int value)
        {
            _price = value;

            if (_price > 0)
                _priceTextObject.SetText($"{value} $");
            else
                _priceTextObject.SetText("Free");
        }
    }
}