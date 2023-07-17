using UnityEngine;
using UnityEngine.Events;

namespace Game.Weapons.Components
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [Header("Weapon components")]
        [Space]
        [SerializeField] private BaseLauncher _launcher;

        [SerializeField] private WeaponVisuals _weaponVisuals;
        [SerializeField] private WeaponState _weaponState;

        [SerializeField] private UnityEvent _shotFired;
        [SerializeField] private UnityEvent _reloadStarted;

        public IWeaponState State => _weaponState;
        public WeaponStats Stats => State.Stats;


        public void ToggleVisual(bool value)
        {
            _weaponVisuals.ToggleEnabled(value);
            Debug.LogWarning($"{gameObject} Visual {value}");
        }

        public void PullOut()
        {
            ToggleVisual(true);
            //Play specific animation
        }

        public void Hide()
        {
            ToggleVisual(false);
            //Play specific animation
        }


        public bool Shoot()
        {
            //Empty mag
            if (State.AmmoInMag <= 0)
                return false;

            State.AmmoInMag--;
            State.TotalAmmo--;

            _launcher.Shoot(State.Deviation);
            _shotFired?.Invoke();

            return true;
        }

        public void Reload()
        {
            //Start visuals, refill mag at the end. Should be breakable
            State.AmmoInMag = Mathf.Min(State.Stats.MagSize, State.TotalAmmo);
            _reloadStarted?.Invoke();
        }
    }
}