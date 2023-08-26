using UnityEngine;

namespace Game.Common
{
    public class TransformComponent : MonoBehaviour, ITransformComponent
    {
        [SerializeField] private Transform _positionTransform;
        [SerializeField] private Transform _turnTransform;

        public Transform MainTransform => _positionTransform;

        public Vector3 Position { get => _positionTransform.position; set => SetPosition(value); }

        public Quaternion Rotation { get => _turnTransform.rotation; set => SetRotation(value); }
        public Vector3 Direction { get => _turnTransform.forward; set => SetDirection(value); }


        void OnDisable()
        {
            //Reset rotation on spawn
            Rotation = Quaternion.identity;
        }


        public void SetPosition(Vector3 value)
        {
            _positionTransform.position = value;
        }

        public void SetRotation(Quaternion value)
        {
            _turnTransform.rotation = value;
        }

        public void SetDirection(Vector3 value)
        {
            var direction2d = value;
            direction2d.y = 0;

            _turnTransform.forward = direction2d;
        }
    }
}