using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons.Components
{
    public static class FireModes
    {
        public static IEnumerator Single(IWeapon weapon, Action onFireCallback = null, Action endCallback = null)
        {
            var cooldown = new WaitForSeconds(1 / weapon.Stats.RateOfFire);

            if (weapon.Shoot())
            {
                onFireCallback?.Invoke();
                yield return cooldown;
            }

            endCallback?.Invoke();
        }

        public static IEnumerator Burst(IWeapon weapon, int amount, Action onFireCallback = null, Action endCallback = null)
        {
            var cooldown = new WaitForSeconds(1 / weapon.Stats.RateOfFire);

            for (int i = 0; i < amount; i++)
                if (weapon.Shoot())
                {
                    onFireCallback?.Invoke();
                    yield return cooldown;
                }

            endCallback?.Invoke();
        }

        public static IEnumerator Auto(IWeapon weapon, Func<bool> condition, Action onFireCallback = null, Action endCallback = null)
        {
            var cooldown = new WaitForSeconds(1 / weapon.Stats.RateOfFire);

            while (condition.Invoke() && weapon.Shoot())
            {
                onFireCallback?.Invoke();
                yield return cooldown;
            }

            endCallback?.Invoke();
        }


        public static IEnumerator Single(IProjectileLauncher launcher, float shootCooldown, Action onFireCallback = null, Action endCallback = null)
        {
            var cooldown = new WaitForSeconds(shootCooldown);

            launcher.Shoot(0);
            onFireCallback?.Invoke();

            yield return cooldown;

            endCallback?.Invoke();
        }

        public static IEnumerator Burst(IProjectileLauncher launcher, int amount, float shootCooldown, Action onFireCallback = null, Action endCallback = null)
        {
            var cooldown = new WaitForSeconds(shootCooldown);

            for (int i = 0; i < amount; i++)
            {
                launcher.Shoot(0);
                onFireCallback?.Invoke();

                yield return cooldown;
            }

            endCallback?.Invoke();
        }

        public static IEnumerator Burst(IWeapon current, object burstAmount, Action endCallback)
        {
            throw new NotImplementedException();
        }
    }
}