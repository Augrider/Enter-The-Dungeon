using System.Collections.Generic;
using Game.Items;

namespace Game.Player
{
    public interface IPlayerItems
    {
        void PickItem(IItem item);
        void DropItem(string itemID);
    }
}