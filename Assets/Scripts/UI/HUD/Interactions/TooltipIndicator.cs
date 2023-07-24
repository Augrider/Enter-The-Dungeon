using TMPro;
using UnityEngine;

namespace Game.UI.HUD
{
    public class TooltipIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tooltipTextObject;
        [Multiline, SerializeField] private string _tooltipText;


        private void OnEnable()
        {
            TooltipComponent.TooltipEnabled += OnTooltipEnabled;
            TooltipComponent.TooltipDisabled += OnTooltipDisabled;
        }

        private void OnDisable()
        {
            TooltipComponent.TooltipEnabled -= OnTooltipEnabled;
            TooltipComponent.TooltipDisabled -= OnTooltipDisabled;
        }



        private void OnTooltipEnabled(string tooltipText)
        {
            var text = string.Format(_tooltipText, tooltipText);
            _tooltipTextObject.SetText(text);

            _tooltipTextObject.enabled = true;
        }

        private void OnTooltipDisabled()
        {
            _tooltipTextObject.enabled = false;
        }
    }
}