namespace Game.Items
{
    [System.Serializable]
    public struct ItemData
    {
        public string Name;

        public ItemSlot ItemSlot;
        public ItemType ItemType;


        // public static bool operator ==(ItemData a, ItemData b)
        // {
        //     return a.Name.Equals(b.Name, System.StringComparison.Ordinal) && a.ItemSlot == b.ItemSlot && a.ItemType == b.ItemType;
        // }

        // public static bool operator !=(ItemData a, ItemData b)
        // {
        //     return !a.Name.Equals(b.Name, System.StringComparison.Ordinal) || a.ItemSlot != b.ItemSlot || a.ItemType != b.ItemType;
        // }
    }
}