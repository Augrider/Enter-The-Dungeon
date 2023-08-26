using System.Collections.Generic;
using System.Linq;
using Game.Plugins.TimeProcesses;
using Game.Weapons;

namespace Game.Player.Components
{
    //TODO: Fix access violations and change overall interface for weapons (to better support work without player unit)
    public class PlayerWeapons : PlayerComponent, IPlayerWeapons
    {
        /// <summary>
        /// Dictionary containing weapon ID and current ammo
        /// </summary>
        private Dictionary<string, int> _weaponsData = new Dictionary<string, int>();

        public int Count => _weaponsData.Count;
        public IWeapon Current => throw new System.FieldAccessException("Weapons cannot be accessed from Player storage!");


        public PlayerWeapons(Player player) : base(player)
        {
        }


        public void AddWeapon(IWeapon weapon)
        {
            if (!_weaponsData.ContainsKey(weapon.ID))
                _weaponsData.Add(weapon.ID, weapon.State.TotalAmmo);

            // PlayerEvents.InvokeWeaponsChanged(Player);
        }

        public void RemoveWeapon(IWeapon weapon)
        {
            _weaponsData.Remove(weapon.ID);

            // PlayerEvents.InvokeWeaponsChanged(Player);
        }


        public bool TryGetWeapon(string weaponID, out IWeapon weapon)
        {
            throw new System.MethodAccessException("Weapons cannot be accessed from Player storage!");
        }


        internal WeaponData[] Export()
        {
            return _weaponsData.Select(t => new WeaponData(t.Key, t.Value)).ToArray();
        }

        internal void Import(PlayerStateData data)
        {
            _weaponsData.Clear();

            foreach (var weaponData in data.WeaponData)
                _weaponsData.Add(weaponData.ItemID, weaponData.Ammo);
        }
    }
}