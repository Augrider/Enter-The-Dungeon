using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Plugins.ObjectPool
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        private Dictionary<T, List<T>> _objects = new Dictionary<T, List<T>>();


        public T GetNew(T prefab)
        {
            T result = null;

            if (!_objects.ContainsKey(prefab) || !_objects[prefab].Any(t => !t.gameObject.activeSelf))
                result = CreateNew(prefab);
            else
                result = _objects[prefab].First(t => !t.gameObject.activeSelf);

            result.gameObject.SetActive(true);

            OnNewActivated(result);
            return result;
        }

        //Note: special component/any component that does destroy object can just disable it instead
        public void Remove(T behavior)
        {
            behavior.gameObject.SetActive(false);
        }


        protected abstract void OnNewActivated(T result);



        private T CreateNew(T prefab)
        {
            var result = Instantiate<T>(prefab, transform);

            if (_objects.ContainsKey(prefab))
                _objects[prefab].Add(result);
            else
                _objects.Add(prefab, new List<T>() { result });

            return result;
        }
    }
}