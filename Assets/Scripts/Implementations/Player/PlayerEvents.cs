using System;
using UnityEngine;

namespace Game.Player.Components
{
    public class PlayerEvents : MonoBehaviour, IPlayerEvents
    {
        public static event Action<IPlayer> PlayerSpawned;
        public static event Action<IPlayer> PlayerDied;

        public event Action<int> HealthChanged;

        public event Action InventoryChanged;

        public event Action WeaponFired;
        public event Action WeaponSwitched;
        public event Action WeaponReloaded;


        public static void InvokePlayerSpawned(IPlayer player) => PlayerSpawned?.Invoke(player);
        public static void InvokePlayerDied(IPlayer player) => PlayerDied?.Invoke(player);

        public void InvokeHealthChanged(int value) => HealthChanged?.Invoke(value);

        public void InvokeInventoryChanged() => InventoryChanged?.Invoke();

        public void InvokeWeaponFired() => WeaponFired?.Invoke();
        public void InvokeWeaponSwitched() => WeaponSwitched?.Invoke();
        public void InvokeWeaponStartedReload() => WeaponReloaded?.Invoke();
    }
}