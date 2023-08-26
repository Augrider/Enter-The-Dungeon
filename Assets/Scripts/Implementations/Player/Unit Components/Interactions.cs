using System.Collections.Generic;
using System.Linq;
using Game.Common;
using UnityEngine;

namespace Game.Player.Components
{
    public class Interactions : MonoBehaviour
    {
        public IInteractable CurrentInteractable => _currentHighlighted;

        private List<IInteractable> _interactables = new List<IInteractable>();
        private IInteractable _currentHighlighted;


        void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IInteractable>(out var component))
                return;

            _interactables.Add(component);
            // RefreshHighlight();
        }

        void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<IInteractable>(out var component))
                return;

            _interactables.Remove(component);
            // RefreshHighlight();
        }


        private void LateUpdate()
        {
            RefreshHighlightState();
        }



        private void RefreshHighlightState()
        {
            var interactables = _interactables.OrderBy(t => GetSqrDistance(t)).Where(t => t.Enabled);

            if (interactables.Count() <= 0)
            {
                SetNewInteractable(null);
                return;
            }

            SetNewInteractable(interactables.First());

            _interactables.RemoveAll(t => !t.Enabled);
        }


        private float GetSqrDistance(IInteractable interactable)
        {
            return (interactable.Position - transform.position).sqrMagnitude;
        }

        private void SetNewInteractable(IInteractable interactable)
        {
            if (interactable == _currentHighlighted)
                return;

            Debug.LogWarning($"Disabling tooltip from {_currentHighlighted}, Enabling tooltip from {interactable}");

            _currentHighlighted?.ToggleHighlight(false);
            _currentHighlighted = interactable;
            _currentHighlighted?.ToggleHighlight(true);
        }
    }
}