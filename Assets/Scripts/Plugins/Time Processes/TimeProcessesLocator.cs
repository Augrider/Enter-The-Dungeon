using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Plugins.TimeProcesses
{
    public static class TimeProcessLocator
    {
        private static ITimeProcessController NullService { get; } = new NullTimeProcessController();
        public static ITimeProcessController Service { get; private set; } = NullService;


        public static void Provide(ITimeProcessController value)
        {
            Service = value != null ? value : NullService;
        }
    }
}