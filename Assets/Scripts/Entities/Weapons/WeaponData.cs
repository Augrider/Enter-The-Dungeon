namespace Game.Weapons
{
    [System.Serializable]
    /// <summary>
    /// Contains all weapon data that needs to be saved
    /// </summary>
    public struct WeaponData
    {
        public string ItemID;
        public int Ammo;


        public WeaponData(string itemID, int ammo)
        {
            ItemID = itemID;
            Ammo = ammo;
        }
    }
}