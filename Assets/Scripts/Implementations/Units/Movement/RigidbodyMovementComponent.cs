using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Units.Components
{
    public class RigidbodyMovementComponent : UnitComponent
    {
        // [SerializeField] private UnityEvent<Vector3> _speedChanged;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _rotationSpeed;

        private Vector3 _targetDirection;
        private Coroutine _turnProcess;

        // private Vector3


        public void SetPaused()
        {
            _rigidbody.isKinematic = true;

            StopAllCoroutines();
            _turnProcess = null;
        }

        public void SetResumed()
        {
            _rigidbody.isKinematic = false;
            // RotateTowards(_targetDirection);
        }


        public void SetSpeed(Vector3 value)
        {
            _rigidbody.AddForce(value, ForceMode.Acceleration);
        }

        public void RotateTowards(Vector3 value)
        {
            _targetDirection = (value - Unit.State.Position).normalized;

            if (_turnProcess == null)
                _turnProcess = StartCoroutine(TurnProcess());
        }


        private IEnumerator TurnProcess()
        {
            while (_targetDirection != Unit.State.Direction)
            {
                StepRotateTowards(_targetDirection, Time.deltaTime);
                yield return null;
            }

            _turnProcess = null;
        }

        //Note: we can just set rotation instead, since it's shooter and this component is for player
        //But having just fast rotation speed gives a bit of weight to movement
        private void StepRotateTowards(Vector3 direction, float deltaTime)
        {
            var direction2d = direction;
            direction2d.y = 0;

            var currentRotation = Unit.State.Rotation;
            var targetRotation = Quaternion.LookRotation(direction2d, Vector3.up);

            Unit.State.Rotation = Quaternion.Lerp(currentRotation, targetRotation, _rotationSpeed * deltaTime);
        }
    }
}