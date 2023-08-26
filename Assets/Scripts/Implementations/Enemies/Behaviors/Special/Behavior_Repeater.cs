using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies.Components
{
    /// <summary>
    /// Special enemy behavior that, while enabled, switches provided behavior on
    /// </summary>
    public class Behavior_Repeater : EnemyBehavior
    {
        [SerializeField] private EnemyBehavior _behavior;


        protected override void OnPlay()
        {
            //Set first or random as active
            _behavior.Enabled = true;
        }

        protected override void OnStop()
        {
        }


        void Update()
        {
            if (!_behavior.Enabled)
                _behavior.Enabled = true;
        }
    }
}