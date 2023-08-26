using System;

namespace Game.Player
{
    public static class PlayerEvents
    {
        public static event Action PlayerSpawned;
        public static event Action PlayerDied;

        public static event Action<int> HealthChanged;
        public static event Action StatsChanged;

        public static event Action InventoryChanged;

        //Events for weapons need to be useful
        public static event Action WeaponFired;
        public static event Action WeaponStartedReloading;
        public static event Action WeaponsChanged;


        public static void InvokePlayerSpawned() => PlayerSpawned?.Invoke();
        public static void InvokePlayerDied() => PlayerDied?.Invoke();

        public static void InvokeHealthChanged(int value) => HealthChanged?.Invoke(value);

        public static void InvokeInventoryChanged() => InventoryChanged?.Invoke();

        public static void InvokeWeaponFired() => WeaponFired?.Invoke();
        public static void InvokeWeaponStartedReloading() => WeaponStartedReloading?.Invoke();
        public static void InvokeWeaponsChanged() => WeaponsChanged?.Invoke();

        public static void InvokeStatsChanged() => StatsChanged?.Invoke();
    }
}