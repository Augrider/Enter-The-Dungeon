using Game.Player;
using Game.Units;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies.Components
{
    //Basic component that structured on condition-action base
    public abstract class EnemyBehavior : MonoBehaviour, IEnemyBehavior
    {
        //TODO: Enchancements to Enemy Behaviors system in general:

        //Make system resistant to object switch off (i.e. never reset shooting cooldowns)
        //Make behaviors have none of state saved during switch offs (i.e. "ghost" bullets on respawning)
        //Create some way to reset multi behavior state or make behaviors more standalone (i.e. shooting bursts with cooldown in between)
        //Fix or find solution for interconnected systems (one always enabled)
        //Give access for events in code?
        //Make behaviors compatible with pause?

        [SerializeField] private BaseEvents _baseEvents;

        public bool Enabled { get => enabled; set => enabled = value; }

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