using System.Collections;
using UnityEngine;

namespace Game.Plugins.TimeProcesses.Components
{
    public abstract class CoroutineProcess : ITimeProcess
    {
        protected Coroutine CurrentCoroutine { get; set; }
        protected MonoBehaviour CoroutineBehavior { get; private set; }

        public bool InProgress { get; protected set; }


        public CoroutineProcess(MonoBehaviour coroutineBehavior)
        {
            CoroutineBehavior = coroutineBehavior;
        }


        public virtual void Stop()
        {
            if (!InProgress)
                return;

            InProgress = false;
            CoroutineBehavior.StopCoroutine(CurrentCoroutine);
        }

        public virtual void StartProcess()
        {
            InProgress = true;

            CurrentCoroutine = CoroutineBehavior.StartCoroutine(Process());
        }


        protected abstract IEnumerator Process();
    }
}