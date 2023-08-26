using System.Collections;
using Game.Common;
using UnityEngine;

namespace Game.Units.Components
{
    public class RigidbodyMovementComponent : MonoBehaviour
    {
        [SerializeField] private TransformComponent _transform;
        [SerializeField] private Rigidbody _rigidbody;

        private Vector3 _targetDirection;
        private Coroutine _turnProcess;


        // public void SetPaused()
        // {
        //     _rigidbody.isKinematic = true;

        //     StopAllCoroutines();
        //     _turnProcess = null;
        // }

        // public void SetResumed()
        // {
        //     _rigidbody.isKinematic = false;
        //     // RotateTowards(_targetDirection);
        // }


        public void SetAcceleration(Vector3 value)
        {
            _rigidbody.AddForce(value, ForceMode.Acceleration);
        }

        public void RotateTowards(Vector3 value, float rotationSpeed)
        {
            _targetDirection = (value - _transform.Position).normalized;

            if (_turnProcess == null)
                _turnProcess = StartCoroutine(TurnProcess(rotationSpeed));
        }


        private IEnumerator TurnProcess(float rotationSpeed)
        {
            while (_targetDirection != _transform.Direction)
            {
                StepRotateTowards(_targetDirection, rotationSpeed, Time.deltaTime);
                yield return null;
            }

            _turnProcess = null;
        }

        //Note: we can just set rotation instead, since it's shooter and this component is for player
        //But having just fast rotation speed gives a bit of weight to movement
        private void StepRotateTowards(Vector3 direction, float rotationSpeed, float deltaTime)
        {
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            _transform.Rotation = Quaternion.Lerp(_transform.Rotation, targetRotation, rotationSpeed * deltaTime);
        }
    }
}