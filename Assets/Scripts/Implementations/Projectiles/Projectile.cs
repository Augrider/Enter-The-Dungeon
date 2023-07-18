using UnityEngine;

namespace Game.Projectiles.Components
{
    public enum ProjectileState { Idle, Active, Destroyed }

    public abstract class Projectile : MonoBehaviour
    {
        public ProjectileState ProjectileState { get; private set; } = ProjectileState.Idle;


        protected void OnDisable()
        {
            SetProjectileState(ProjectileState.Destroyed);
        }


        public void SetProjectileState(ProjectileState value)
        {
            ProjectileState = value;

            switch (value)
            {
                case ProjectileState.Idle:
                    SetIdleState();
                    break;

                case ProjectileState.Active:
                    SetActiveState();
                    break;

                case ProjectileState.Destroyed:
                    SetDestroyedState();
                    break;
            }
        }


        protected abstract void SetIdleState();
        protected abstract void SetActiveState();
        protected abstract void SetDestroyedState();
    }
}