using Game.Player;
using Game.Player.Components;
using TMPro;
using UnityEngine;

namespace Game.UI.HUD
{
    public class CurrencyIndicator : PlayerElement
    {
        [SerializeField] private TextMeshProUGUI _currencyTextObject;
        [Multiline, SerializeField] private string _currencyText;

        private int _current;


        void OnEnable()
        {
            Debug.Log($"Money {Players.Current.State.Currency}");
            PlayerEvents.InventoryChanged += OnInventoryChanged;

            _current = Player.State.Currency;
            OnInventoryChanged();
        }

        // void OnDisable()
        // {
        //     Player.Events.InventoryChanged -= OnInventoryChanged;
        // }



        private void OnInventoryChanged()
        {
            // if (_current == Players.Current.Inventory.Currency)
            //     return;

            _current = Players.Current.State.Currency;

            var healthText = string.Format(_currencyText, $"{_current}");
            _currencyTextObject.SetText(healthText);
        }
    }
}