using System.Collections;
using UnityEngine;

namespace Game.Enemies.Components
{
    public class Behavior_Switch : MonoBehaviour
    {
        [SerializeField] private EnemyBehavior[] _behaviors;
        [SerializeField] private EnemyBehavior _current;

        [SerializeField] private float _delay;
        [SerializeField] private bool _controlGameObjects;

        bool _idle = true;


        private void Start()
        {
            if (_current != null)
                ToggleActiveBehavior(_current, true);
        }


        public void SelectNew()
        {
            if (!_idle)
                StopAllCoroutines();

            var behavior = _behaviors[Random.Range(0, _behaviors.Length)];
            StartCoroutine(SwitchProcess(behavior));
        }

        public void SelectNew(EnemyBehavior behavior)
        {
            if (!_idle)
                StopAllCoroutines();

            StartCoroutine(SwitchProcess(behavior));
        }



        private IEnumerator SwitchProcess(EnemyBehavior behavior)
        {
            _idle = false;

            ToggleActiveBehavior(_current, false);

            yield return new WaitForSeconds(_delay);

            _current = behavior;
            ToggleActiveBehavior(_current, true);

            _idle = true;
        }


        private void ToggleActiveBehavior(EnemyBehavior behavior, bool value)
        {
            if (behavior == null)
                return;

            behavior.Enabled = value;

            if (_controlGameObjects)
                behavior.gameObject.SetActive(value);
        }
    }
}