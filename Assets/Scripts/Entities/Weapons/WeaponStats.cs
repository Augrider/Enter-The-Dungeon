using UnityEngine;

namespace Game.Weapons
{
    [System.Serializable]
    public struct WeaponStats
    {
        public FireMode FireMode;

        [Min(0.01f)]
        public float RateOfFire;

        [Min(0)]
        public float ReloadCooldown;
        // [Min(0)]
        // public float PullOutCooldown;

        public bool InfiniteMag;
        [Min(1)]
        public int MagSize;

        public bool InfiniteAmmo;
        [Min(1)]
        public int MaxAmmo;


        public static WeaponStats operator +(WeaponStats a, WeaponStats b)
        {
            var result = a;

            result.RateOfFire = Mathf.Clamp(result.RateOfFire + b.RateOfFire, 0, float.MaxValue);

            result.ReloadCooldown = Mathf.Clamp(result.ReloadCooldown + b.ReloadCooldown, 0, float.MaxValue);
            // result.PullOutCooldown = Mathf.Clamp(result.PullOutCooldown + b.PullOutCooldown, 0, float.MaxValue);

            result.MaxAmmo = Mathf.Clamp(result.MaxAmmo + b.MaxAmmo, 1, int.MaxValue);
            result.MagSize = Mathf.Clamp(result.MagSize + b.MagSize, 1, result.MaxAmmo);

            return result;
        }

        public static WeaponStats operator -(WeaponStats a, WeaponStats b)
        {
            var result = a;

            result.RateOfFire = Mathf.Clamp(result.RateOfFire - b.RateOfFire, 0, float.MaxValue);

            result.ReloadCooldown = Mathf.Clamp(result.ReloadCooldown - b.ReloadCooldown, 0, float.MaxValue);
            // result.PullOutCooldown = Mathf.Clamp(result.PullOutCooldown - b.PullOutCooldown, 0, float.MaxValue);

            result.MaxAmmo = Mathf.Clamp(result.MaxAmmo - b.MaxAmmo, 1, int.MaxValue);
            result.MagSize = Mathf.Clamp(result.MagSize - b.MagSize, 1, result.MaxAmmo);

            return result;
        }
    }
}