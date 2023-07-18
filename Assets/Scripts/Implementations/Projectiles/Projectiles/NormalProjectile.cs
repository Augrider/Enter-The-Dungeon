using System.Collections;
using Game.Plugins.TimeProcesses;
using UnityEngine;

namespace Game.Projectiles.Components
{
    public class NormalProjectile : Projectile
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _projectileSpeed;

        [SerializeField] private float _lifetime;


        protected override void SetActiveState()
        {
            StopAllCoroutines();
            StartCoroutine(MoveProcess());
        }

        protected override void SetDestroyedState()
        {
            StopAllCoroutines();

            _rigidbody.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }

        protected override void SetIdleState()
        {
            StopAllCoroutines();
            _rigidbody.velocity = Vector3.zero;
        }



        private IEnumerator MoveProcess()
        {
            var time = 0f;

            _rigidbody.AddForce(_projectileSpeed * transform.forward, ForceMode.VelocityChange);

            while (time < _lifetime)
            {
                time += Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }

            SetDestroyedState();
        }
    }
}