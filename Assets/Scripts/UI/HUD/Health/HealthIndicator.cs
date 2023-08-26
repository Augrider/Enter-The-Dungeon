using Game.Player;
using TMPro;
using UnityEngine;

namespace Game.UI.HUD
{
    public class HealthIndicator : PlayerElement
    {
        [SerializeField] private TextMeshProUGUI _healthTextObject;
        [Multiline, SerializeField] private string _healthText;

        private int _currentHealth;


        void OnEnable()
        {
            PlayerEvents.HealthChanged += OnHealthChanged;
            OnHealthChanged(Player.State.Health);
        }

        // void OnDisable()
        // {
        //     Player.Events.HealthChanged -= OnHealthChanged;
        // }



        private void OnHealthChanged(int value)
        {
            _currentHealth = value;

            var healthText = string.Format(_healthText, $"{_currentHealth}/{Players.Current.Stats.MaxHealth}");
            _healthTextObject.SetText(healthText);
        }
    }
}