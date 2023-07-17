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
                StartCoroutine(FireModes.Burst(launcher, _burstAmount, _shootCooldown, endCallback: SwitchEnabled));
        }

        protected override void OnStop()
        {
            StopAllCoroutines();
        }
    }
}