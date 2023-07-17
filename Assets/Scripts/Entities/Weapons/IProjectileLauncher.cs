using UnityEngine;

namespace Game.Weapons
{
    /// <summary>
    /// Base interface for all projectile creators
    /// </summary>
    public interface IProjectileLauncher
    {
        void Shoot(float deviation);
    }
}