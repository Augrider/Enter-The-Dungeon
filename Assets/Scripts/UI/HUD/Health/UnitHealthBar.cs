using Game.Units.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.HUD
{
    public class UnitHealthBar : UnitComponent
    {
        //TODO: Give Destructible reference and make it more generic
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Slider _slider;


        private void OnEnable()
        {
            Unit.Destructible.HealthChanged += OnUnitHealthChanged;
            OnUnitHealthChanged(Unit.Destructible.Health);
        }

        private void OnDisable()
        {
            Unit.Destructible.HealthChanged -= OnUnitHealthChanged;
        }


        private void Update()
        {
            transform.rotation = Quaternion.identity;
        }



        private void OnUnitHealthChanged(int health)
        {
            _slider.SetValueWithoutNotify((float)health / Unit.Destructible.MaxHealth);
        }
    }
}