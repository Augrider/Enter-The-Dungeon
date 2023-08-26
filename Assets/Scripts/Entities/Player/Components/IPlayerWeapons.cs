using Game.Items;
using Game.Weapons;

namespace Game.Player
{
    public interface IPlayerWeapons
    {
        IWeapon Current { get; }
        int Count { get; }

        /// <summary>
        /// Add weapon to the list
        /// </summary>
        /// <param name="weapon">Weapon reference</param>
        void AddWeapon(IWeapon weapon);
        /// <summary>
        /// Remove weapon from the list
        /// </summary>
        /// <param name="weapon">Weapon reference</param>
        void RemoveWeapon(IWeapon weapon);

        /// <summary>
        /// Try to get weapon attached to this item
        /// </summary>
        /// <param name="weapon">A weapon granted by getting provided item</param>
        /// <returns>Is provided item gave this player a weapon?</returns>
        bool TryGetWeapon(string weaponID, out IWeapon weapon);
    }
}