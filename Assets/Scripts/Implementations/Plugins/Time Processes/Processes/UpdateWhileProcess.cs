using System;
using System.Collections;
using UnityEngine;

namespace Game.Plugins.TimeProcesses.Components
{
    public class UpdateWhileProcess : CoroutineProcess
    {
        private Func<bool> _condition;
        private Action _updateAction;
        private Action _afterAction;


        public UpdateWhileProcess(MonoBehaviour coroutineBehavior, Func<bool> condition, Action updateAction, Action afterAction = null) : base(coroutineBehavior)
        {
            _condition = condition;
            _updateAction = updateAction;
            _afterAction = afterAction;
        }


        protected override IEnumerator Process()
        {
            while (_condition.Invoke())
            {
                _updateAction?.Invoke();
                yield return null;
            }

            _afterAction?.Invoke();
            InProgress = false;
        }
    }
}