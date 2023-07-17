using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Projectiles.Components
{
    public abstract class ProjectileComponent : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;
        public Projectile Projectile => _projectile;
    }
}