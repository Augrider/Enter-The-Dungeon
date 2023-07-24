using Game.Units.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.HUD
{
    public class UnitHealthBar : UnitComponent
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Slider _slider;


        private void OnEnable()
        {
            Unit.StateChanged += OnUnitStateChanged;
            OnUnitStateChanged();
        }

        private void OnDisable()
        {
            Unit.StateChanged -= OnUnitStateChanged;
        }


        private void Update()
        {
            transform.rotation = Quaternion.identity;
        }



        private void OnUnitStateChanged()
        {
            _slider.SetValueWithoutNotify((float)Unit.State.Health / Unit.State.MaxHealth);
        }
    }
}