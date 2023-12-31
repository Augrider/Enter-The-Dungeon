using System;

namespace Game.Plugins.TimeProcesses
{
    /// <summary>
    /// Interface for objects that can start time processes
    /// </summary>
    public interface ITimeProcessController
    {
        ITimeProcess DoNextFrame(Action action);
        ITimeProcess DoAfterDelay(Action action, float delay);
        ITimeProcess DoAfter(Action action, Func<bool> condition);

        ITimeProcess UpdateWhile(Action action, Func<bool> condition, Action afterAction = null);
        ITimeProcess LateUpdateWhile(Action action, Func<bool> condition, Action afterAction = null);
        ITimeProcess FixedUpdateWhile(Action action, Func<bool> condition, Action afterAction = null);
    }
}