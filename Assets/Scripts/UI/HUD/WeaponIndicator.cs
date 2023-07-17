using Game.Player;
using Game.Plugins.TimeProcesses;
using Game.Weapons;
using TMPro;
using UnityEngine;

namespace Game.UI.HUD
{
    public class WeaponIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _weaponTextObject;
        [Multiline, SerializeField] private string _weaponText;

        private IWeapon CurrentWeapon => Players.Current.Weapons.Current;


        void OnEnable()
        {
            Players.Current.Events.InventoryChanged += OnWeaponStateChanged;

            Players.Current.Events.WeaponFired += OnWeaponStateChanged;
            Players.Current.Events.WeaponSwitched += OnWeaponStateChanged;

            Players.Current.Events.WeaponReloaded += OnWeaponReloading;

            OnWeaponStateChanged();
        }

        void OnDisable()
        {
            if (!Players.IsCurrentAlive)
                return;

            Players.Current.Events.InventoryChanged -= OnWeaponStateChanged;

            Players.Current.Events.WeaponFired -= OnWeaponStateChanged;
            Players.Current.Events.WeaponSwitched -= OnWeaponStateChanged;

            Players.Current.Events.WeaponReloaded -= OnWeaponReloading;
        }



        private void OnWeaponStateChanged()
        {
            string totalAmmo = CurrentWeapon.Stats.InfiniteAmmo ? "Inf" : CurrentWeapon.State.TotalAmmo.ToString();
            // string AmmoInMag = CurrentWeapon.Stats.InfiniteAmmo ? "Inf" : CurrentWeapon.State.TotalAmmo.ToString();

            var weaponText = string.Format(_weaponText, $"{CurrentWeapon.State.AmmoInMag}/{totalAmmo}");
            _weaponTextObject.SetText(weaponText);
        }

        private void OnWeaponReloading()
        {
            var weaponText = string.Format(_weaponText, "Reloading");
            _weaponTextObject.SetText(weaponText);

            TimeProcessLocator.Service.DoAfterDelay(OnWeaponStateChanged, CurrentWeapon.Stats.ReloadCooldown);
        }
    }
}