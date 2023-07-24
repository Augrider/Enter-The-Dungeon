using System;
using Game.Player;
using Game.State;
using Game.Units;
using Game.Units.Components;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies.Components
{
    public class EnemyBehaviorEvents : MonoBehaviour
    {
        private enum State { Normal, Far, Close, NoVisual }

        [SerializeField] protected LineOfSight _lineOfSight;

        [SerializeField] private float _playerCloseRange;
        [SerializeField] private float _playerFarRange;

        [SerializeField] private UnityEvent ObjectEnabled;
        [SerializeField] private UnityEvent ObjectDisabled;

        [SerializeField] private EnemyStateEvents _stateEvents;

        private State _currentState;

        public bool GotPlayerVisual { get; private set; }

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

            if (GameStateLocator.Service.Paused)
                return;

            bool visual = CheckPlayerVisual();
            SetCurrentVisual(visual);

            if (!IsPlayerAlive)
                return;

            switch (_currentState)
            {
                // case State.NoVisual:
                //     OnNoVisual();
                //     return;

                case State.Close:
                    OnClose();
                    return;

                case State.Far:
                    OnFar();
                    return;

                case State.Normal:
                    OnNormal();
                    return;
            }
        }


        private void OnNormal()
        {
            if (CheckPlayerClose(_playerCloseRange))
            {
                _stateEvents.PlayerClose?.Invoke();
                _currentState = State.Close;
                return;
            }

            if (CheckPlayerFar(_playerFarRange))
            {
                _stateEvents.PlayerFar?.Invoke();
                _currentState = State.Far;
            }
        }

        private void OnFar()
        {
            if (!CheckPlayerFar(_playerFarRange))
                _currentState = State.Normal;
        }

        private void OnClose()
        {
            if (!CheckPlayerClose(_playerCloseRange))
                _currentState = State.Normal;
        }

        private void OnNoVisual()
        {
            if (GotPlayerVisual)
                _currentState = State.Normal;
        }


        protected bool CheckPlayerClose(float minRange) => SqrDistance < minRange * minRange;
        protected bool CheckPlayerFar(float maxRange) => SqrDistance > maxRange * maxRange;



        private bool CheckPlayerVisual()
        {
            return IsPlayerAlive ? _lineOfSight.CheckLineOfSight(PlayerUnit) : false;
        }

        private void SetCurrentVisual(bool visual)
        {
            if (GotPlayerVisual != visual)
            {
                GotPlayerVisual = visual;

                if (GotPlayerVisual)
                {
                    _stateEvents.RegainedPlayerVisual?.Invoke();
                    _currentState = State.Normal;
                }
                else
                {
                    _stateEvents.LostPlayerVisual?.Invoke();
                    _currentState = State.NoVisual;
                }
            }
        }



        [System.Serializable]
        private struct EnemyStateEvents
        {
            public UnityEvent PlayerClose;
            public UnityEvent PlayerFar;

            public UnityEvent LostPlayerVisual;
            public UnityEvent RegainedPlayerVisual;
        }
    }
}