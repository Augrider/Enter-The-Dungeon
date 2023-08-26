using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies.Components
{
    /// <summary>
    /// Special enemy behavior, that switches between behaviors in sequence
    /// </summary>
    public class Behavior_Switch : EnemyBehavior
    {
        private enum SwitchMode { Sequence, Loop, Random }

        [SerializeField] private EnemyBehavior[] _behaviors;
        private EnemyBehavior _current;

        [SerializeField] private SwitchMode _switchMode;
        [SerializeField] private bool _controlGameObjects;


        protected override void OnPlay()
        {
            //Set first or random as active
            switch (_switchMode)
            {
                default:
                case SwitchMode.Sequence:
                    StartCoroutine(SequenceProcess());
                    return;

                case SwitchMode.Loop:
                    StartCoroutine(LoopProcess());
                    return;

                case SwitchMode.Random:
                    StartCoroutine(RandomProcess());
                    return;
            }
        }

        protected override void OnStop()
        {
            //Reset active
            StopAllCoroutines();
            SwitchActive(null);
        }



        private IEnumerator SequenceProcess()
        {
            for (int i = 0; i < _behaviors.Length; i++)
            {
                SwitchActive(_behaviors[i]);

                while (_current.Enabled)
                    yield return null;
            }

            SwitchEnabled();
        }

        private IEnumerator LoopProcess()
        {
            var index = 0;

            while (true)
            {
                SwitchActive(_behaviors[index]);

                while (_current.Enabled)
                    yield return null;

                index = index + 1 >= _behaviors.Length ? 0 : index + 1;
            }
        }

        private IEnumerator RandomProcess()
        {
            while (true)
            {
                SwitchActive(_behaviors[Random.Range(0, _behaviors.Length)]);

                while (_current.Enabled)
                    yield return null;
            }
        }



        private void SwitchActive(EnemyBehavior next)
        {
            ToggleBehavior(_current, false);
            ToggleBehavior(next, true);

            _current = next;
        }


        private void ToggleBehavior(EnemyBehavior behavior, bool value)
        {
            if (behavior == null)
                return;

            behavior.Enabled = value;

            if (_controlGameObjects)
                behavior.gameObject.SetActive(value);
        }
    }
}