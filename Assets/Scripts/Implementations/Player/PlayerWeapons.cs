using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Items;
using Game.Plugins.Input;
using Game.Plugins.TimeProcesses;
using Game.Weapons;
using Game.Weapons.Components;
using UnityEngine;

namespace Game.Player.Components
{
    public class PlayerWeapons : PlayerComponent, IPlayerWeapons
    {
        //TODO: Better starting inventory of items and weapons
        [Tooltip("Starting weapons on unit. They need to be manually placed on unit object and they cannot be dropped (at least now)")]
        [SerializeField] private Weapon[] _startingWeapons;

        private Dictionary<IWeapon, int> _weaponItems = new Dictionary<IWeapon, int>();

        private IWeapon[] _weapons => _weaponItems.Keys.ToArray();
        private IWeapon _current;

        private ITimeProcess _currentProcess;
        private WeaponActionState _currentState = WeaponActionState.Idle;

        public IWeapon Current => _current;


        void Start()
        {
            foreach (var weapon in _startingWeapons)
                AddWeapon(weapon, -1);
        }


        public void AddWeapon(IWeapon weapon, int weaponID)
        {
            if (_weaponItems.ContainsKey(weapon))
                return;

            _weaponItems.Add(weapon, weaponID);
            SwitchTo(weapon);
        }

        public void RemoveWeapon(IWeapon weapon)
        {
            if (!_weaponItems.ContainsKey(weapon))
                return;

            if (_weaponItems[weapon] < 0)
                return;

            var droppedItem = _weaponItems[weapon];

            _weaponItems.Remove(weapon);
            SwitchTo(_weapons.Last());
        }


        public bool TryGetWeapon(int weaponID, out IWeapon weapon)
        {
            weapon = null;

            if (!_weaponItems.ContainsValue(weaponID))
                return false;

            weapon = _weaponItems.First(t => t.Value == weaponID).Key;
            return true;
        }

        public bool TryGetID(IWeapon weapon, out int weaponID)
        {
            weaponID = -1;

            if (!_weaponItems.ContainsKey(weapon))
                return false;

            weaponID = _weaponItems[weapon];
            return weaponID >= 0;
        }


        public void SwitchToNext()
        {
            SwitchTo(_weapons.Next(_current));
        }

        public void SwitchToPrevious()
        {
            SwitchTo(_weapons.Previous(_current));
        }


        public void Shoot()
        {
            //Check current state and process, stop if able and shoot
            //Stop the process, also not necessary
            if (_currentState != WeaponActionState.Idle)
                return;

            if (Current.State.AmmoInMag <= 0)
            {
                Reload();
                return;
            }

            _currentState = WeaponActionState.Shooting;

            switch (Current.State.Stats.FireMode)
            {
                case FireMode.Single:
                    StartCoroutine(FireModes.Single(Current, Player.Events.InvokeWeaponFired, SetIdle));
                    return;

                case FireMode.Burst:
                    StartCoroutine(FireModes.Burst(Current, 3, Player.Events.InvokeWeaponFired, SetIdle));
                    return;

                case FireMode.Auto:
                    Func<bool> condition = () => Current.State.AmmoInMag > 0 && InputLocator.Service.ShootInput.IsPressed;
                    StartCoroutine(FireModes.Auto(Current, condition, Player.Events.InvokeWeaponFired, SetIdle));
                    return;
            }
        }

        public void Reload()
        {
            if (_currentState != WeaponActionState.Idle)
                return;

            //Mag is infinite
            if (Current.State.Stats.InfiniteMag)
                return;

            //All available ammo is already inside mag
            if (Current.State.AmmoInMag == Current.State.TotalAmmo)
                return;

            //Mag is full
            if (Current.State.AmmoInMag == Current.State.Stats.MagSize)
                return;

            _currentState = WeaponActionState.Reloading;
            Current.Reload();

            Player.Events.InvokeWeaponStartedReload();
            SetIdleAfterDelay(Current.State.Stats.ReloadCooldown);
        }



        private void SwitchTo(IWeapon weapon)
        {
            if (weapon == _current)
                return;

            _currentState = WeaponActionState.Switching;

            _current?.Hide();
            _current = weapon;
            _current.PullOut();

            Player.Events.InvokeWeaponSwitched();
            // SetIdleAfterDelay(_current.Stats.PullOutCooldown);
            SetIdleAfterDelay(0.01f);
        }


        private void SetIdle() => _currentState = WeaponActionState.Idle;

        private void SetIdleAfterDelay(float delay)
        {
            _currentProcess = TimeProcessLocator.Service.DoAfterDelay(SetIdle, delay);
        }
    }
}