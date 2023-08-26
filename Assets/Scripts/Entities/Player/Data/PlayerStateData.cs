using Game.Weapons;

namespace Game.Player
{
    [System.Serializable]
    /// <summary>
    /// Contains all player state exported into structure
    /// </summary>
    public struct PlayerStateData
    {
        public int Health;

        public int Currency;

        public string[] Items;
        public WeaponData[] WeaponData;
    }
}