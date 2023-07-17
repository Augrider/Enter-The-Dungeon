using System;
using UnityEngine;

namespace Game.Player.Components
{
    public class PlayerEvents : MonoBehaviour, IPlayerEvents
    {
        public event Action<int> HealthChanged;
        public event Action PlayerDied;

        public event Action InventoryChanged;

        public event Action WeaponFired;
        public event Action WeaponSwitched;
        public event Action WeaponReloaded;

        public void InvokeHealthChanged(int value) => HealthChanged?.Invoke(value);
        public void InvokePlayerDied() => PlayerDied?.Invoke();

        public void InvokeInventoryChanged() => InventoryChanged?.Invoke();

        public void InvokeWeaponFired() => WeaponFired?.Invoke();
        public void InvokeWeaponSwitched() => WeaponSwitched?.Invoke();
        public void InvokeWeaponStartedReload() => WeaponReloaded?.Invoke();
    }
}