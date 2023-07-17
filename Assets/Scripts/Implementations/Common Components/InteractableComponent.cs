using Game.Common;
using Game.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Items.Components
{
    /// <summary>
    /// Component used for customizable interactions with player
    /// </summary>
    public class InteractableComponent : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent<IPlayer> _playerNearby;
        [SerializeField] private UnityEvent<IPlayer> _playerLeft;
        [SerializeField] private UnityEvent<IPlayer> _playerInteracted;

        [SerializeField] private UnityEvent<bool> _toggleTooltip;

        public bool Enabled { get => enabled; set => enabled = value; }

        public Vector3 Position => transform.position;


        void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IPlayer>(out var component))
                return;

            _playerNearby?.Invoke(component);
        }

        void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<IPlayer>(out var component))
                return;

            _playerLeft?.Invoke(component);
        }


        public void Interact(IPlayer player)
        {
            _playerInteracted?.Invoke(player);
        }

        public void ToggleHighlight(bool value)
        {
            Debug.Log($"Highlight enabled {value}");
            _toggleTooltip?.Invoke(value);
        }
    }
}