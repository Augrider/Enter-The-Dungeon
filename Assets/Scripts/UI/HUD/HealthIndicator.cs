using Game.Player;
using TMPro;
using UnityEngine;

namespace Game.UI.HUD
{
    public class HealthIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthTextObject;
        [Multiline, SerializeField] private string _healthText;

        private int _currentHealth;


        void OnEnable()
        {
            _currentHealth = Players.Current.State.Health;
            Players.Current.Events.HealthChanged += OnHealthChanged;

            OnHealthChanged(_currentHealth);
        }

        void OnDisable()
        {
            if (Players.IsCurrentAlive)
                Players.Current.Events.HealthChanged -= OnHealthChanged;
        }



        private void OnHealthChanged(int value)
        {
            _currentHealth = value;

            var healthText = string.Format(_healthText, $"{_currentHealth}/{Players.Current.Stats.MaxHealth}");
            _healthTextObject.SetText(healthText);
        }
    }
}