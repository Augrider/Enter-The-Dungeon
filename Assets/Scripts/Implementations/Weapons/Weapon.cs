using Game.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Weapons.Components
{
    //TODO: Integrate ID from item, maybe simplify
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private IDComponent _id;

        [Header("Weapon components")]
        [Space]
        [SerializeField] private BaseLauncher _launcher;

        [SerializeField] private WeaponVisuals _weaponVisuals;
        [SerializeField] private WeaponState _weaponState;

        [SerializeField] private UnityEvent _shotFired;
        [SerializeField] private UnityEvent _reloadStarted;

        public string ID => _id.Value;

        public IWeaponState State => _weaponState;
        public WeaponStats Stats => State.Stats;


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



        private void ToggleVisual(bool value)
        {
            _weaponVisuals.ToggleEnabled(value);
            Debug.LogWarning($"{gameObject} Visual {value}");
        }
    }
}