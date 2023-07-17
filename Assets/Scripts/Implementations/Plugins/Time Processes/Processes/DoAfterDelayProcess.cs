using System;
using System.Collections;
using UnityEngine;

namespace Game.Plugins.TimeProcesses.Components
{
    public class DoAfterDelayProcess : CoroutineProcess
    {
        private float _delay;
        private Action _action;


        public DoAfterDelayProcess(MonoBehaviour coroutineBehavior, float delay, Action action) : base(coroutineBehavior)
        {
            _delay = delay;
            _action = action;
        }


        protected override IEnumerator Process()
        {
            yield return new WaitForSeconds(_delay);

            _action?.Invoke();
            InProgress = false;
        }
    }
}