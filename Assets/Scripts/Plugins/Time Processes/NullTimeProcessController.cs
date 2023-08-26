using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Plugins.TimeProcesses
{
    internal sealed class NullTimeProcessController : ITimeProcessController
    {
        public ITimeProcess DoAfter(Action action, Func<bool> condition)
        {
            throw new NotImplementedException();
        }

        public ITimeProcess DoAfterDelay(Action action, float delay)
        {
            throw new NotImplementedException();
        }

        public ITimeProcess DoNextFrame(Action action)
        {
            throw new NotImplementedException();
        }

        public ITimeProcess FixedUpdateWhile(Action action, Func<bool> condition, Action afterAction = null)
        {
            throw new NotImplementedException();
        }

        public ITimeProcess LateUpdateWhile(Action action, Func<bool> condition, Action afterAction = null)
        {
            throw new NotImplementedException();
        }

        public ITimeProcess UpdateWhile(Action action, Func<bool> condition, Action afterAction = null)
        {
            throw new NotImplementedException();
        }
    }
}