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

        private IWeapon CurrentWeapon => Player.Weapons.Current;


        void OnEnable()
        {
            PlayerEvents.WeaponsChanged += OnWeaponStateChanged;
            PlayerEvents.WeaponStartedReloading += OnWeaponReloading;

            OnWeaponStateChanged();
        }

        void OnDisable()
        {
            PlayerEvents.WeaponsChanged -= OnWeaponStateChanged;
            PlayerEvents.WeaponStartedReloading -= OnWeaponReloading;
        }



        private void OnWeaponStateChanged()
        {
            Debug.Log($"Showing update for {CurrentWeapon}");
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