using Game.Units.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.HUD
{
    public class BossIndicator : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TextMeshProUGUI _nameTextObject;
        [SerializeField] private Slider _healthBar;

        private Unit _current;


        private void OnEnable()
        {
            BossComponent.BossIndicatorEnabled += OnBossEnabled;
            BossComponent.BossIndicatorDisabled += OnBossDisabled;
        }

        private void OnDisable()
        {
            BossComponent.BossIndicatorEnabled -= OnBossEnabled;
            BossComponent.BossIndicatorDisabled -= OnBossDisabled;
        }


        private void OnBossEnabled(string bossName, Unit unit)
        {
            _nameTextObject.SetText(bossName);
            _current = unit;

            _current.StateChanged += OnUnitStateChanged;
            OnUnitStateChanged();

            _canvas.enabled = true;
        }

        private void OnBossDisabled()
        {
            _current.StateChanged -= OnUnitStateChanged;
            _current = null;

            _canvas.enabled = false;
        }


        private void OnUnitStateChanged()
        {
            _healthBar.SetValueWithoutNotify((float)_current.State.Health / _current.State.MaxHealth);
        }
    }
}