using System.Collections;
using UnityEngine;

namespace Game.Projectiles.Components
{
    public class NormalProjectile : Projectile
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _projectileSpeed;

        [SerializeField] private float _lifetime;

        bool _paused;
        private Vector3 _speed;


        public override void TogglePause(bool value)
        {
            if (_paused == value)
                return;

            _paused = value;

            if (value)
            {
                _speed = _rigidbody.velocity;
                _rigidbody.isKinematic = true;
            }
            else
            {
                _rigidbody.isKinematic = false;
                _rigidbody.velocity = _speed;
            }
        }


        protected override void SetActiveState()
        {
            StopAllCoroutines();
            StartCoroutine(MoveProcess(_projectileSpeed * transform.forward));
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


        private IEnumerator MoveProcess(Vector3 speed)
        {
            var time = 0f;

            _rigidbody.velocity = speed;

            while (time < _lifetime)
            {
                if (!_paused)
                    time += Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }

            SetDestroyedState();
        }
    }
}