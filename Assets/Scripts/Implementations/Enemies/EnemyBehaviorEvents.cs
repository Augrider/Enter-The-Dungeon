using System;
using Game.Player;
using Game.State;
using Game.Units;
using Game.Units.Components;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies.Components
{
    public sealed class EnemyBehaviorEvents : MonoBehaviour
    {
        private enum State { InRange, Far, Close, NoVisual }

        [SerializeField] private LineOfSight _lineOfSight;

        [SerializeField] private float _playerCloseRange;
        [SerializeField] private float _playerFarRange;

        [SerializeField] private EnemyStateEvents _stateEvents;

        private State _currentState = State.NoVisual;

        public bool GotPlayerVisual { get; private set; } = false;

        private bool IsPlayerAlive => Players.IsCurrentAlive;

        private Vector3 PlayerPosition => Players.Current.Position;
        private float SqrDistance => (PlayerPosition - transform.position).sqrMagnitude;


        void LateUpdate()
        {
            if (GameStateLocator.Service.Paused)
                return;

            if (!IsPlayerAlive)
                return;

            bool visual = _lineOfSight.CheckLineOfSight(PlayerPosition);
            SetCurrentVisual(visual);

            switch (_currentState)
            {
                case State.NoVisual:
                    OnNoVisual();
                    return;

                case State.Close:
                    OnClose();
                    return;

                case State.Far:
                    OnFar();
                    return;

                case State.InRange:
                    OnInRange();
                    return;
            }
        }


        private void OnInRange()
        {
            if (CheckPlayerClose(_playerCloseRange))
            {
                SetState(State.Close);
                return;
            }

            if (CheckPlayerFar(_playerFarRange))
                SetState(State.Far);
        }

        private void OnFar()
        {
            if (!CheckPlayerFar(_playerFarRange))
                SetState(State.InRange);
        }

        private void OnClose()
        {
            if (!CheckPlayerClose(_playerCloseRange))
                SetState(State.InRange);
        }

        private void OnNoVisual()
        {
            if (!GotPlayerVisual)
                return;

            if (CheckPlayerClose(_playerCloseRange))
            {
                SetState(State.Close);
                return;
            }

            if (CheckPlayerFar(_playerFarRange))
            {
                SetState(State.Far);
                return;
            }

            SetState(State.InRange);
        }



        private void SetCurrentVisual(bool visual)
        {
            if (GotPlayerVisual == visual)
                return;

            GotPlayerVisual = visual;

            if (!GotPlayerVisual)
                SetState(State.NoVisual);
        }


        private void SetState(State value)
        {
            if (value == _currentState)
                return;

            _currentState = value;

            switch (_currentState)
            {
                case State.NoVisual:
                    _stateEvents.LostPlayerVisual?.Invoke();
                    _stateEvents.PlayerOutOfRange?.Invoke();
                    return;

                case State.Close:
                    _stateEvents.RegainedPlayerVisual?.Invoke();
                    _stateEvents.PlayerOutOfRange?.Invoke();
                    _stateEvents.PlayerClose?.Invoke();
                    return;

                case State.Far:
                    _stateEvents.RegainedPlayerVisual?.Invoke();
                    _stateEvents.PlayerOutOfRange?.Invoke();
                    _stateEvents.PlayerFar?.Invoke();
                    return;

                case State.InRange:
                    _stateEvents.RegainedPlayerVisual?.Invoke();
                    _stateEvents.PlayerInRange?.Invoke();
                    return;
            }
        }


        private bool CheckPlayerClose(float minRange) => SqrDistance < minRange * minRange;
        private bool CheckPlayerFar(float maxRange) => SqrDistance > maxRange * maxRange;


        [System.Serializable]
        private struct EnemyStateEvents
        {
            public UnityEvent PlayerClose;
            public UnityEvent PlayerFar;

            public UnityEvent PlayerInRange;
            public UnityEvent PlayerOutOfRange;

            public UnityEvent LostPlayerVisual;
            public UnityEvent RegainedPlayerVisual;
        }
    }
}