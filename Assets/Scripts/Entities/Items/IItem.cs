using Game.Player;
using UnityEngine;

namespace Game.Items
{
    public interface IItem
    {
        int ID { get; }
        ItemData Data { get; }

        void SetPosition(Vector3 position, Quaternion rotation);
        void SetPosition(Transform parent, Vector3 position, Quaternion rotation);
        // void ToggleVisual(bool value);
        void Destroy();

        void OnItemPicked(IPlayer player);
        void OnItemDropped(IPlayer player);

        //TODO: Later inventory specific components can be added to units (if enemies can do something with them)
        //Otherwise it's okay to work only with players for now
    }
}