using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Plugins.ObjectPool
{
    public class ObjectPoolLocator : MonoBehaviour
    {
        private static IObjectPool NullService { get; } = new NullObjectPool();
        public static IObjectPool Service { get; private set; } = NullService;


        public static void Provide(IObjectPool value)
        {
            Service = value != null ? value : NullService;
        }
    }
}