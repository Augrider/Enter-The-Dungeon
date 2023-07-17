using System.Collections.Generic;
using System.Linq;
using Game.Common;
using UnityEngine;

namespace Game.Player.Components
{
    public class PlayerInteractions : PlayerComponent, IPlayerInteractions
    {
        public IInteractable CurrentInteractable => _currentHighlighted;

        private List<IInteractable> _interactables = new List<IInteractable>();
        private IInteractable _currentHighlighted;


        void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IInteractable>(out var component))
                return;

            _interactables.Add(component);
            RefreshHighlight();
        }

        void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<IInteractable>(out var component))
                return;

            _interactables.Remove(component);
            RefreshHighlight();
        }


        //Optionally add late update for highlight refresh


        public bool TryInteractWith(IInteractable interactable)
        {
            if (!interactable.Enabled)
            {
                RefreshHighlight();
                return false;
            }

            Debug.Log($"Interacting with {interactable}");

            if (_interactables.Contains(interactable))
                _interactables.Remove(interactable);

            interactable.Interact(Player);

            RefreshHighlight();
            return true;
        }



        private void RefreshHighlight()
        {
            var interactables = _interactables.OrderBy(t => GetSqrDistance(t)).Where(t => t.Enabled);

            if (interactables.Count() <= 0)
            {
                SetNewInteractable(null);
                return;
            }

            SetNewInteractable(interactables.First());
            Debug.Log($"New selectable {_currentHighlighted}");
        }

        private float GetSqrDistance(IInteractable interactable)
        {
            return (interactable.Position - Unit.State.Position).sqrMagnitude;
        }

        private void SetNewInteractable(IInteractable interactable)
        {
            _currentHighlighted?.ToggleHighlight(false);
            _currentHighlighted = interactable;
            _currentHighlighted?.ToggleHighlight(true);
        }
    }
}