using System;
using System.Collections;
using UnityEngine;

namespace Game.Plugins.TimeProcesses.Components
{
    public class DoNextFrameProcess : CoroutineProcess
    {
        private Action _action;


        public DoNextFrameProcess(MonoBehaviour coroutineBehavior, Action action) : base(coroutineBehavior)
        {
            _action = action;
        }


        protected override IEnumerator Process()
        {
            yield return null;

            _action?.Invoke();
            InProgress = false;
        }
    }
}