using UnityEngine;

namespace Game.Enemies.Components
{
    public class Behavior_Complex : EnemyBehavior
    {
        [SerializeField] private EnemyBehavior[] _behaviorsOnStart;
        [SerializeField] private EnemyBehavior[] _nestedBehaviors;


        protected override void OnPlay()
        {
            foreach (var behavior in _behaviorsOnStart)
                behavior.Enabled = true;

            // gameObject.SetActive(true);
        }

        protected override void OnStop()
        {
            foreach (var behavior in _nestedBehaviors)
                behavior.Enabled = false;

            // gameObject.SetActive(false);
        }
    }
}