using UnityEngine;

namespace Game.Projectiles.Components
{
    public class RotateProjectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Vector3 _angularVelocity;


        void Start()
        {
            _rigidbody.AddTorque(_angularVelocity, ForceMode.VelocityChange);
        }
    }
}