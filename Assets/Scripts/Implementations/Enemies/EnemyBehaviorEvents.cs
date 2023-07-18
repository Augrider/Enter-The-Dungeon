using Game.Player;
using Game.Units;
using Game.Units.Components;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies.Components
{
    public class EnemyBehaviorEvents : MonoBehaviour
    {
        [SerializeField] protected LineOfSight _lineOfSight;

        [SerializeField] private float _playerCloseRange;
        [SerializeField] private float _playerFarRange;

        [SerializeField] private UnityEvent ObjectEnabled;
        [SerializeField] private UnityEvent ObjectDisabled;

        [SerializeField] private MovementEvents _movementEvents;


        private bool _currentVisual;

        public bool GotPlayerVisual => _currentVisual;

        private IUnit PlayerUnit => Players.Current?.Unit;
        private bool IsPlayerAlive => Players.IsCurrentAlive;

        private Vector3 PlayerPosition => Players.Current.UnitState.Position;
        private float SqrDistance => (PlayerUnit.State.Position - transform.position).sqrMagnitude;


        private void OnEnable()
        {
            ObjectEnabled?.Invoke();
        }

        private void OnDisable()
        {
            ObjectDisabled?.Invoke();
        }


        void LateUpdate()
        {
            //Scan for events
            //Define which are useless later
            //For too close and too far events ranges needed

            bool visual = CheckPlayerVisual();
            SetCurrentVisual(visual);


            if (!IsPlayerAlive)
                return;

            if (CheckPlayerClose(_playerCloseRange))
                _movementEvents.PlayerClose?.Invoke();

            if (CheckPlayerFar(_playerFarRange))
                _movementEvents.PlayerFar?.Invoke();
        }


        protected bool CheckPlayerClose(float minRange) => SqrDistance < minRange * minRange;
        protected bool CheckPlayerFar(float maxRange) => SqrDistance > maxRange * maxRange;



        private bool CheckPlayerVisual()
        {
            return IsPlayerAlive ? _lineOfSight.CheckLineOfSight(PlayerUnit) : false;
        }

        private void SetCurrentVisual(bool visual)
        {
            if (_currentVisual != visual)
            {
                _currentVisual = visual;

                if (_currentVisual)
                    _movementEvents.RegainedPlayerVisual?.Invoke();
                else
                    _movementEvents.LostPlayerVisual?.Invoke();
            }
        }



        [System.Serializable]
        private struct MovementEvents
        {
            public UnityEvent PlayerClose;
            public UnityEvent PlayerFar;

            public UnityEvent LostPlayerVisual;
            public UnityEvent RegainedPlayerVisual;
        }
    }
}