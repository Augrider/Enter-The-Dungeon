using Game.Player;
using UnityEngine;

namespace Game.Items
{
    public interface IItem
    {
        string ID { get; }
        ItemData Data { get; }

        /// <summary>
        /// Set item position and rotation, reset parent to standard
        /// </summary>
        void SetPosition(Vector3 position, Quaternion rotation);
        /// <summary>
        /// Set item position, rotation and parent
        /// </summary>
        void SetPosition(Transform parent, Vector3 position, Quaternion rotation);

        // void ToggleVisual(bool value);
        void Destroy();

        void OnItemPicked(IPlayer player);
        void OnItemDropped(IPlayer player);
    }
}