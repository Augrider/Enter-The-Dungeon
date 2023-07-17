using System;
using System.Collections;
using UnityEngine;

namespace Game.Plugins.TimeProcesses.Components
{
    public class DoAfterProcess : CoroutineProcess
    {
        private Func<bool> _condition;
        private Action _action;


        public DoAfterProcess(MonoBehaviour coroutineBehavior, Func<bool> condition, Action action) : base(coroutineBehavior)
        {
            _condition = condition;
            _action = action;
        }


        protected override IEnumerator Process()
        {
            yield return new WaitUntil(_condition);

            _action?.Invoke();
            InProgress = false;
        }
    }
}