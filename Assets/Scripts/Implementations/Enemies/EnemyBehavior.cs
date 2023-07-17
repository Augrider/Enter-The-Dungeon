using Game.Player;
using Game.Units;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies.Components
{
    //Basic component that structured on condition-action base
    public abstract class EnemyBehavior : MonoBehaviour, IEnemyBehavior
    {
        //Check for line of sight, it also needed for some movement 
        [SerializeField] private BaseEvents _baseEvents;

        public bool Enabled { get => enabled; set => enabled = value; }

        protected IUnit PlayerUnit => Players.Current?.Unit;
        protected bool IsPlayerAlive => Players.IsCurrentAlive;


        protected void OnEnable()
        {
            Play();
        }

        protected void OnDisable()
        {
            Stop();
        }


        public void ToggleEnabled(bool value) => Enabled = value;
        public void SwitchEnabled() => Enabled = !Enabled;



        private void Play()
        {
            OnPlay();
            _baseEvents.BehaviorStarted?.Invoke();
        }

        private void Stop()
        {
            OnStop();
            _baseEvents.BehaviorStopped?.Invoke();
        }


        protected abstract void OnPlay();
        protected abstract void OnStop();



        [System.Serializable]
        private struct BaseEvents
        {
            public UnityEvent BehaviorStarted;
            public UnityEvent BehaviorStopped;
        }
    }
}