using Game.Player;
using TMPro;
using UnityEngine;

namespace Game.UI.HUD
{
    public class CurrencyIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currencyTextObject;
        [Multiline, SerializeField] private string _currencyText;

        private int _current;


        void OnEnable()
        {
            _current = Players.Current.Inventory.Currency;
            Players.Current.Events.InventoryChanged += OnInventoryChanged;

            OnInventoryChanged();
        }

        void OnDisable()
        {
            if (Players.IsCurrentAlive)
                Players.Current.Events.InventoryChanged -= OnInventoryChanged;
        }



        private void OnInventoryChanged()
        {
            // if (_current == Players.Current.Inventory.Currency)
            //     return;

            _current = Players.Current.Inventory.Currency;

            var healthText = string.Format(_currencyText, $"{_current}");
            _currencyTextObject.SetText(healthText);
        }
    }
}