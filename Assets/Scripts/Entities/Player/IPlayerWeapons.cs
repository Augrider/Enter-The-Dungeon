using Game.Items;
using Game.Weapons;

namespace Game.Player
{
    public interface IPlayerWeapons
    {
        IWeapon Current { get; }

        /// <summary>
        /// Add weapon to the list
        /// </summary>
        /// <param name="weapon">Weapon reference</param>
        /// <param name="weaponItem">Item attached to weapon, can be null</param>
        void AddWeapon(IWeapon weapon, int weaponID);
        /// <summary>
        /// Remove weapon from the list
        /// </summary>
        void RemoveWeapon(IWeapon weapon);

        /// <summary>
        /// Try to get weapon attached to this item
        /// </summary>
        /// <param name="weapon">A weapon granted by getting provided item</param>
        /// <returns>Is provided item gave this player weapon?</returns>
        bool TryGetWeapon(int weaponID, out IWeapon weapon);
        /// <summary>
        /// Try to get item ID by weapon attached to this item
        /// </summary>
        /// <param name="weapon">A weapon reference</param>
        /// <param name="weaponID"></param>
        /// <returns>Is provided weapon currently on player and does have item attached?</returns>
        bool TryGetID(IWeapon weapon, out int weaponID);

        //TODO: Simplify this after moving action state out of this class
        void SwitchToNext();
        void SwitchToPrevious();

        //TODO: Move shoot and reload to player action state class (not as useful here)
        void Shoot();
        void Reload();
    }
}