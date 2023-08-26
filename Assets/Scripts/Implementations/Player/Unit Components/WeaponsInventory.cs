using System.Collections.Generic;
using System.Linq;
using Game.Weapons;
using UnityEngine;

namespace Game.Player.Components
{
    public class WeaponsInventory : PlayerUnitComponent, IPlayerWeapons
    {
        private List<IWeapon> _weapons = new List<IWeapon>();

        // public IEnumerable<IWeapon> Weapons => _weapons;
        public int Count => _weapons.Count;

        public IWeapon Current { get; private set; }


        public void AddWeapon(IWeapon weapon)
        {
            if (_weapons.Contains(weapon))
                return;

            _weapons.Add(weapon);
            Actions.SwitchWeaponTo(weapon);
        }

        public void RemoveWeapon(IWeapon weapon)
        {
            if (!_weapons.Contains(weapon))
                return;

            _weapons.Remove(weapon);
            Actions.SwitchWeaponTo(_weapons.Last());
        }


        public bool TryGetWeapon(string weaponID, out IWeapon weapon)
        {
            weapon = _weapons.FirstOrDefault(t => t.ID == weaponID);
            return weapon != null;
        }


        internal void Clear()
        {
            _weapons.Clear();
        }


        public bool Contains(string weaponID) => _weapons.Any(t => t.ID == weaponID);


        public IWeapon GetWeapon(string weaponID)
        {
            return _weapons.First(t => t.ID == weaponID);
        }

        internal IWeapon GetNext()
        {
            return _weapons.Next(Current);
        }

        internal IWeapon GetPrevious()
        {
            return _weapons.Previous(Current);
        }


        public void SwitchTo(IWeapon weapon)
        {
            if (weapon == Current)
                return;

            Current?.Hide();
            Current = weapon;
            Current.PullOut();
        }


        internal WeaponData[] Export()
        {
            return _weapons.Select(t => new WeaponData(t.ID, t.State.TotalAmmo)).ToArray();
        }

        internal void Import(PlayerStateData data)
        {
            //Since weapons state saved here, _weaponsInventory should be accessed after import for data sync
            foreach (var weaponData in data.WeaponData)
            {
                if (TryGetWeapon(weaponData.ItemID, out var weapon))
                    weapon.State.TotalAmmo = weaponData.Ammo;
            }
        }
    }
}