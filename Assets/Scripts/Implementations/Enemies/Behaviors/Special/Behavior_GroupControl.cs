using UnityEngine;

namespace Game.Enemies.Components
{
    public class Behavior_GroupControl : EnemyBehavior
    {
        //TODO: Leave as EnemyBehavior, make it useful

        //It should always reset when Game Object is disabled and enable when active (check if works)
        //Only use it as a means to save state of pattern when it enabled/disabled
        
        [SerializeField] private EnemyBehavior[] _behaviorsToEnable;
        [SerializeField] private EnemyBehavior[] _behaviorsToDisable;


        protected override void OnPlay()
        {
            foreach (var behavior in _behaviorsToEnable)
                behavior.Enabled = true;

            // gameObject.SetActive(true);
        }

        protected override void OnStop()
        {
            foreach (var behavior in _behaviorsToDisable)
                behavior.Enabled = false;

            // gameObject.SetActive(false);
        }
    }
}