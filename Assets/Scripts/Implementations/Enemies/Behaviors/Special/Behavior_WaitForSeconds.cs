using System.Collections;
using UnityEngine;

namespace Game.Enemies.Components
{
    public class Behavior_WaitForSeconds : EnemyBehavior
    {
        [SerializeField] private float _waitTime;


        protected override void OnPlay()
        {
            StartCoroutine(Wait(_waitTime));
        }

        protected override void OnStop()
        {
            StopAllCoroutines();
        }



        private IEnumerator Wait(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SwitchEnabled();
        }
    }
}