using Game.Units.Components;
using UnityEngine;

namespace Game.UI.HUD
{
    public class BossComponent : UnitComponent
    {
        public static event System.Action<string, Unit> BossIndicatorEnabled;
        public static event System.Action BossIndicatorDisabled;

        [SerializeField] private string _bossName;


        public void ToggleBossIndicator(bool value)
        {
            if (value)
                BossIndicatorEnabled?.Invoke(_bossName, Unit);
            else
                BossIndicatorDisabled?.Invoke();
        }
    }
}