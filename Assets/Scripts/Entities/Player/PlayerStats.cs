using UnityEngine;

namespace Game.Player
{
    [System.Serializable]
    public struct PlayerStats
    {
        //Here are placed player specific parameters. Items usually change only those
        //Max health is also should be moved here, and state will have exposed Max Health for setting and reading
        private const int HEALTH_MAX_VALUE = 100;
        private const float SPEED_DEFAULT = 16;


        [Min(1)]
        public int MaxHealth;

        [Range(0.2f, 2)]
        public float SpeedMultiplier;
        public float Speed => SPEED_DEFAULT * SpeedMultiplier;


        public static PlayerStats operator +(PlayerStats a, PlayerStats b)
        {
            var result = a;

            result.MaxHealth = Mathf.Clamp(result.MaxHealth + b.MaxHealth, 1, HEALTH_MAX_VALUE);
            result.SpeedMultiplier = Mathf.Clamp(result.SpeedMultiplier + b.SpeedMultiplier, 0.2f, 2);

            return result;
        }

        public static PlayerStats operator -(PlayerStats a, PlayerStats b)
        {
            var result = a;

            result.MaxHealth = Mathf.Clamp(result.MaxHealth - b.MaxHealth, 1, HEALTH_MAX_VALUE);
            result.SpeedMultiplier = Mathf.Clamp(result.SpeedMultiplier - b.SpeedMultiplier, 0.2f, 2);

            return result;
        }
    }
}