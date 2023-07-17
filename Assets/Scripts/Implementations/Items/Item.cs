using Game.Common;
using Game.Player;
using UnityEngine;

namespace Game.Items.Components
{
    public abstract class Item : MonoBehaviour, IItem, IInteractable
    {
        [SerializeField] private ItemData _data;

        [Header("Item physics components")]
        [Space]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        public Transform DefaultParent { get; set; }

        public int ID { get; set; }

        public ItemData Data => _data;

        public bool Enabled { get => enabled; set => enabled = value; }
        public Vector3 Position => transform.position;


        public void ToggleHighlight(bool value)
        {
            Debug.Log($"Highlight enabled {value}");
        }

        public void Interact(IPlayer player)
        {
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

        public void TogglePhysics(bool value)
        {
            _rigidbody.isKinematic = !value;
            _collider.enabled = value;
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }


        void IItem.OnItemPicked(IPlayer player)
        {
            //Common logic
            OnItemPicked(player);
        }

        void IItem.OnItemDropped(IPlayer player)
        {
            //Common logic
            OnItemDropped(player);
        }


        protected abstract void OnItemPicked(IPlayer player);
        protected abstract void OnItemDropped(IPlayer player);


        public override bool Equals(object other)
        {
            if (!(other is Item item))
                return false;

            return item.Data == this.Data;
        }

        public static bool operator ==(Item a, Item b)
        {
            return a.Data == b.Data;
        }

        public static bool operator !=(Item a, Item b)
        {
            return a.Data != b.Data;
        }
    }
}