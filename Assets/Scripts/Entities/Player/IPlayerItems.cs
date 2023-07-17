using System.Collections.Generic;
using Game.Items;

namespace Game.Player
{
    public interface IPlayerItems
    {
        int Currency { get; set; }

        void PickItem(IItem item);

        void DropItem(IItem item);
        void DropItem(int itemID);

        IEnumerable<IItem> GetItems();
    }
}