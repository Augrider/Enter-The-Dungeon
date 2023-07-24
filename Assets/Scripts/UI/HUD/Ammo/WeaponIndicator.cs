using Game.Player;
using Game.Plugins.TimeProcesses;
using Game.Weapons;
using TMPro;
using UnityEngine;

namespace Game.UI.HUD
{
    public class WeaponIndicator : PlayerElement
    {
        [SerializeField] private TextMeshProUGUI _weaponTextObject;
        [Multiline, SerializeField] private string _weaponText;

        private IWeapon CurrentWeapon => Players.Current.Weapons.Current;


        void OnEnable()
        {
            Player.Events.InventoryChanged += OnWeaponStateChanged;

            Player.Events.WeaponFired += OnWeaponStateChanged;
            Player.Events.WeaponSwitched += OnWeaponStateChanged;

            Player.Events.WeaponReloaded += OnWeaponReloading;

            OnWeaponStateChanged();
        }

        // void OnDisable()
        // {
        //     Player.Events.InventoryChanged -= OnWeaponStateChanged;

        //     Player.Events.WeaponFired -= OnWeaponStateChanged;
        //     Player.Events.WeaponSwitched -= OnWeaponStateChanged;

        //     Player.Events.WeaponReloaded -= OnWeaponReloading;
        // }



        private void OnWeaponStateChanged()
        {
            var ammoText = string.Format(_weaponText, "∞");

            if (CurrentWeapon == null)
                return;

            if (!CurrentWeapon.Stats.InfiniteMag || !CurrentWeapon.Stats.InfiniteAmmo)
            {
                string totalAmmoText = CurrentWeapon.Stats.InfiniteAmmo ? "∞" : CurrentWeapon.State.TotalAmmo.ToString();
                ammoText = string.Format(_weaponText, $"{CurrentWeapon.State.AmmoInMag}/{totalAmmoText}");
            }

            _weaponTextObject.SetText(ammoText);
        }

        private void OnWeaponReloading()
        {
            var weaponText = string.Format(_weaponText, "Reloading");
            _weaponTextObject.SetText(weaponText);

            TimeProcessLocator.Service.DoAfterDelay(OnWeaponStateChanged, CurrentWeapon.Stats.ReloadCooldown);
        }
    }
}