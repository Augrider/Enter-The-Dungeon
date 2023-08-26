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
        [SerializeField] private bool _interactOnce;
        [SerializeField] private bool _disableOnPlayerNearby;
        [SerializeField] private bool _disableOnPlayerLeft;

        [SerializeField] private UnityEvent<IPlayer> _playerNearby;
        [SerializeField] private UnityEvent<IPlayer> _playerLeft;
        [SerializeField] private UnityEvent<IPlayer> _playerInteracted;

        [SerializeField] private UnityEvent<bool> _toggleTooltip;

        public bool Enabled { get => enabled; set => enabled = value; }

        public Vector3 Position => transform.position;


        private void OnDisable()
        {
            ToggleHighlight(false);
        }


        void OnTriggerEnter(Collider other)
        {
            if (!Enabled || !other.TryGetComponent<IPlayer>(out var player))
                return;

            _playerNearby?.Invoke(player);

            if (_disableOnPlayerNearby)
                enabled = false;
        }

        void OnTriggerExit(Collider other)
        {
            if (!Enabled || !other.TryGetComponent<IPlayer>(out var player))
                return;

            _playerLeft?.Invoke(player);

            if (_disableOnPlayerLeft)
                enabled = false;
        }


        public void Interact(IPlayer player)
        {
            _playerInteracted?.Invoke(player);

            if (_interactOnce)
                enabled = false;
        }


        public void ToggleHighlight(bool value)
        {
            _toggleTooltip?.Invoke(value);
        }
    }
}