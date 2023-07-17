using Game.Units;
using Game.Units.Components;
using UnityEngine;

namespace Game.Enemies.Components
{
    public class LineOfSight : MonoBehaviour
    {
        [SerializeField] private LayerMask _obstacleMask;
        [SerializeField] private float _maxRange;
        [Tooltip("Ray origin offset in local coordinates")]
        [SerializeField] private Vector3 _castOffset;

        [Tooltip("Radius of the sphere casted from unit to point. Recommended to be less than unit itself")]
        [SerializeField] private float _visionSphereRadius;
        [Tooltip("Defines how shorter the range of the sphere cast is compared to max range. Smaller range help with detection of units standing right next to obstacle")]
        [SerializeField, Range(0, 1)] private float _sphereCastRangeMultiplier;

        private Vector3 CastOffsetWorld => transform.TransformVector(_castOffset);


        /// <summary>
        /// Check if own unit can see provided unit (no obstacles in between)
        /// </summary>
        /// <param name="targetUnit">Unit that needs to be seen</param>
        /// <returns>True if no obstacles on the line of sight</returns>
        public bool CheckLineOfSight(IUnit targetUnit)
        {
            return CheckLineOfSight(targetUnit.State.Position);
        }


        /// <summary>
        /// Check if own unit can see provided point (no obstacles in between)
        /// </summary>
        /// <param name="target">Point that needs to be seen</param>
        /// <returns>True if no obstacles on the line of sight</returns>
        public bool CheckLineOfSight(Vector3 target)
        {
            var targetVector = target - transform.position;

            if (targetVector.sqrMagnitude > _maxRange * _maxRange)
                return false;

            var ray = new Ray(transform.position + CastOffsetWorld, targetVector);
            Debug.DrawLine(transform.position + CastOffsetWorld, target + _castOffset, Color.green, 0.1f);

            if (Physics.Raycast(ray, targetVector.magnitude, _obstacleMask))
                return false;

            return !Physics.SphereCast(ray, _visionSphereRadius, targetVector.magnitude * _sphereCastRangeMultiplier - _visionSphereRadius, _obstacleMask);
        }
    }
}