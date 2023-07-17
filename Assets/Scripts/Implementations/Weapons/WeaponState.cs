using UnityEngine;

namespace Game.Weapons.Components
{
    public class WeaponState : MonoBehaviour, IWeaponState
    {
        [SerializeField] private WeaponStats _stats;

        [Space]
        [SerializeField] private int _ammoInMag;
        [SerializeField] private int _totalAmmo;

        public int AmmoInMag { get => GetAmmoInMag(); set => SetAmmoInMag(value); }
        public int TotalAmmo { get => GetTotalAmmo(); set => SetTotalAmmo(value); }

        private int MaxAmmo => Stats.InfiniteAmmo ? int.MaxValue : Stats.MaxAmmo;
        private int MagSize => Stats.InfiniteMag ? MaxAmmo : Stats.MagSize;

        private int CurrentMaxMagSize => Mathf.Min(TotalAmmo, MagSize);

        public float RecoilPercentage { get; set; } = 0;
        public float Deviation { get; set; } = 0;

        public WeaponStats Stats => _stats;


        public void ApplyStatsChange(WeaponStats stats)
        {
            _stats += stats;
        }


        public void SetTotalAmmo(int value)
        {
            _totalAmmo = Mathf.Clamp(value, 0, MaxAmmo);
        }

        public void SetAmmoInMag(int value)
        {
            _ammoInMag = Mathf.Clamp(value, 0, CurrentMaxMagSize);
        }



        private int GetAmmoInMag()
        {
            return Stats.InfiniteMag ? GetTotalAmmo() : _ammoInMag;
        }

        private int GetTotalAmmo()
        {
            return Stats.InfiniteAmmo ? int.MaxValue : _totalAmmo;
        }
    }
}