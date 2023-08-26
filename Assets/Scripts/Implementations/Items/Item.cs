using Game.Common;
using Game.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Items.Components
{
    //TODO: Inherit from Interactable? Or inherit only some features
    public abstract class Item : MonoBehaviour, IItem, IInteractable
    {
        [SerializeField] private IDComponent _id;

        [SerializeField] private ItemData _data;

        [Header("Item physics components")]
        [Space]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        [Space]
        [SerializeField] private UnityEvent<bool> _highlightToggled;
        [SerializeField] private UnityEvent<bool> _visualsToggled;

        public static Transform DefaultParent { get; set; }

        public string ID { get => _id.Value; }

        public ItemData Data => _data;

        public bool Enabled { get => enabled; set => enabled = value; }
        public Vector3 Position => transform.position;


        public virtual void Interact(IPlayer player)
        {
            // enabled = false;
            player.Inventory.PickItem(this);
        }


        public void SetPosition(Vector3 position, Quaternion rotation)
        {
            transform.parent = DefaultParent;

            transform.position = position;
            transform.rotation = rotation;
        }

        public void SetPosition(Transform parent, Vector3 position, Quaternion rotation)
        {
            transform.parent = parent;

            transform.position = position;
            transform.rotation = rotation;
        }


        public void ToggleHighlight(bool value)
        {
            _highlightToggled?.Invoke(value);
        }

        public void TogglePhysics(bool value)
        {
            _rigidbody.isKinematic = !value;
            _collider.enabled = value;
        }

        public void ToggleVisual(bool value)
        {
            _visualsToggled?.Invoke(value);
        }


        /// <summary>
        /// Removes item instance from game
        /// </summary>
        public void Destroy()
        {
            enabled = false;
            gameObject.SetActive(false);
        }


        void IItem.OnItemPicked(IPlayer player)
        {
            //Common logic
            enabled = false;
            OnItemPicked(player);
        }

        void IItem.OnItemDropped(IPlayer player)
        {
            //Common logic
            enabled = true;
            ToggleVisual(true);
            TogglePhysics(true);

            OnItemDropped(player);
        }


        protected abstract void OnItemPicked(IPlayer player);
        protected abstract void OnItemDropped(IPlayer player);


        // public override bool Equals(object other)
        // {
        //     if (other == null)
        //         return false;

        //     if (!(other is Item item))
        //         return false;

        //     return this.Data.Equals(item.Data);
        // }

        // public static bool operator ==(Item a, Item b)
        // {
        //     if (object.ReferenceEquals(a, null))
        //         return object.ReferenceEquals(b, null);

        //     return a.Equals(b);
        // }

        // public static bool operator !=(Item a, Item b)
        // {
        //     if (object.ReferenceEquals(a, null))
        //         return !object.ReferenceEquals(b, null);

        //     return !a.Equals(b);
        // }
    }
}