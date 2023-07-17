using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Plugins.TimeProcesses.Components
{
    public class TimeProcessController : MonoBehaviour, ITimeProcessController
    {
        private List<CoroutineProcess> _processes = new List<CoroutineProcess>();


        void Awake()
        {
            TimeProcessLocator.Provide(this);
        }

        void OnDestroy()
        {
            TimeProcessLocator.Provide(null);
        }

        void OnDisable()
        {
            StopAllProcesses();
        }

        void LateUpdate()
        {
            RemoveFinishedProcesses();
        }


        public ITimeProcess DoAfter(Action action, Func<bool> condition)
        {
            var process = new DoAfterProcess(this, condition, action);
            StartProcess(process);

            return process;
        }

        public ITimeProcess DoAfterDelay(Action action, float delay)
        {
            var process = new DoAfterDelayProcess(this, delay, action);
            StartProcess(process);

            return process;
        }


        public ITimeProcess LateUpdateWhile(Action action, Func<bool> condition, Action afterAction = null)
        {
            var process = new LateUpdateWhileProcess(this, condition, action, afterAction);
            StartProcess(process);

            return process;
        }

        public ITimeProcess UpdateWhile(Action action, Func<bool> condition, Action afterAction = null)
        {
            var process = new UpdateWhileProcess(this, condition, action, afterAction);
            StartProcess(process);

            return process;
        }

        public ITimeProcess FixedUpdateWhile(Action action, Func<bool> condition, Action afterAction = null)
        {
            var process = new FixedUpdateWhileProcess(this, condition, action, afterAction);
            StartProcess(process);

            return process;
        }



        private void StartProcess(CoroutineProcess process)
        {
            // Debug.Log("Process started");
            _processes.Add(process);
            process.StartProcess();
        }


        private void StopAllProcesses()
        {
            foreach (var process in _processes.ToArray())
                process.Stop();

            _processes.Clear();
        }

        private void RemoveFinishedProcesses()
        {
            foreach (var process in _processes.ToArray())
                if (!process.InProgress)
                {
                    // Debug.Log("Removing stopped process");
                    _processes.Remove(process);
                }
        }
    }
}