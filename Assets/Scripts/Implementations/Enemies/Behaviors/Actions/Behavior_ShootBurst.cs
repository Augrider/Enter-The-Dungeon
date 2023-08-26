using System.Collections;
using Game.Weapons.Components;
using UnityEngine;

namespace Game.Enemies.Components
{
    public class Behavior_ShootBurst : EnemyBehavior
    {
        //Enemies can use launchers directly to simplify calculations and setup
        //Launchers can still trigger animations with Unity Events

        [SerializeField] private BaseLauncher[] _launchers;

        [SerializeField] private int _burstAmount;
        [SerializeField] private float _shootCooldown;


        protected override void OnPlay()
        {
            foreach (var launcher in _launchers)
                StartCoroutine(BurstProcess());
        }

        protected override void OnStop()
        {
            StopAllCoroutines();
        }



        private IEnumerator BurstProcess()
        {
            yield return null;
            var cooldown = new WaitForSeconds(_shootCooldown);

            for (int i = 0; i < _burstAmount; i++)
            {
                foreach (var launcher in _launchers)
                    launcher.Shoot(0);

                yield return cooldown;
            }

            yield return new WaitForSeconds(_shootCooldown * _burstAmount);

            SwitchEnabled();
        }
    }
}