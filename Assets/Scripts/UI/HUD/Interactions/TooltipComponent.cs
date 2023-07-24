using UnityEngine;

namespace Game.UI.HUD
{
    public class TooltipComponent : MonoBehaviour
    {
        public static event System.Action<string> TooltipEnabled;
        public static event System.Action TooltipDisabled;

        [SerializeField] private string _tooltipText;

        bool _toggled = false;


        private void OnDisable()
        {
            if (_toggled)
                ToggleTooltip(false);
        }


        public void ToggleTooltip(bool value)
        {
            if (!enabled)
                return;

            _toggled = value;

            if (value)
                TooltipEnabled?.Invoke(_tooltipText);
            else
                TooltipDisabled?.Invoke();
        }
    }
}