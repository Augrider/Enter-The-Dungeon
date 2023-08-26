using System;
using System.Collections;
using Game.Plugins.Input;
using Game.Units.Components;
using Game.Weapons;
using Game.Weapons.Components;
using UnityEngine;

namespace Game.Player.Components
{
    public enum ActionState { Idle, Shooting, Reloading, Switching }


    /// <summary>
    /// Handles state of action for unit and player
    /// </summary>
    public class PlayerUnitActions : PlayerUnitComponent
    {
        private const float SPEED_DEFAULT = 16;
        private const float ROTATION_SPEED_DEFAULT = 200;

        [SerializeField] private RigidbodyMovementComponent _movementComponent;
        [SerializeField] private float _switchCooldown;

        private float _cooldown;

        public ActionState State { get; private set; }

        //Note: various states can change speed
        public float MovementSpeedMultiplier { get; private set; } = 1;
        public float RotationSpeedMultiplier { get; private set; } = 1;

        private float Speed => MovementSpeedMultiplier * PlayerUnit.Stats.SpeedMultiplier * SPEED_DEFAULT;
        private float RotationSpeed => RotationSpeedMultiplier * ROTATION_SPEED_DEFAULT;

        private IWeapon CurrentWeapon => PlayerUnit.Weapons.Current;


        public void ControlMovement(Vector2 movementInput, Vector3 lookPoint)
        {
            //Not do that if rolling or dashing?

            _movementComponent.RotateTowards(lookPoint, RotationSpeed);

            var movement = Speed * new Vector3(movementInput.x, 0, movementInput.y);

            if (movement.sqrMagnitude > 0)
                _movementComponent.SetAcceleration(movement);
        }


        public void Shoot()
        {
            if (State != ActionState.Idle)
                return;

            if (PlayerUnit.Weapons.Count < 1)
                return;

            if (CurrentWeapon.State.AmmoInMag <= 0)
            {
                Reload();
                return;
            }

            State = ActionState.Shooting;

            switch (CurrentWeapon.Stats.FireMode)
            {
                case FireMode.Single:
                    StartCoroutine(FireModes.Single(CurrentWeapon, OnWeaponFired, endCallback: SetIdle));
                    return;

                case FireMode.Burst:
                    StartCoroutine(FireModes.Burst(CurrentWeapon, CurrentWeapon.Stats.BurstAmount, OnWeaponFired, endCallback: SetIdle));
                    return;

                case FireMode.Auto:
                    Func<bool> condition = () => InputLocator.Service.ShootInput.IsPressed;
                    StartCoroutine(FireModes.Auto(CurrentWeapon, condition, OnWeaponFired, endCallback: SetIdle));
                    return;
            }
        }

        public void Reload()
        {
            if (State != ActionState.Idle)
                return;

            if (PlayerUnit.Weapons.Count < 1)
                return;

            if (!IsAvailableToReload(CurrentWeapon))
                return;

            CurrentWeapon.Reload();
            OnWeaponStartedReloading();

            ChangeState(ActionState.Reloading, CurrentWeapon.Stats.ReloadCooldown);
        }

        public void SwitchWeaponToPrevious()
        {
            if (PlayerUnit.Weapons.Count <= 1)
                return;

            SwitchWeaponTo(PlayerUnit.Weapons.GetPrevious());
        }

        public void SwitchWeaponToNext()
        {
            if (PlayerUnit.Weapons.Count <= 1)
                return;

            SwitchWeaponTo(PlayerUnit.Weapons.GetNext());
        }

        public void SwitchWeaponTo(IWeapon weapon)
        {
            PlayerUnit.Weapons.SwitchTo(weapon);
            OnWeaponsChanged();

            ChangeState(ActionState.Switching, _switchCooldown);
        }



        private void OnWeaponFired()
        {
            OnWeaponsChanged();
            PlayerEvents.InvokeWeaponFired();
        }

        private void OnWeaponStartedReloading()
        {
            OnWeaponsChanged();
            PlayerEvents.InvokeWeaponStartedReloading();
        }

        private void OnWeaponsChanged() => PlayerEvents.InvokeWeaponsChanged();


        private void SetIdle() => State = ActionState.Idle;

        private void ChangeState(ActionState state, float cooldown)
        {
            StopAllCoroutines();
            _cooldown = cooldown;
            StartCoroutine(ChangeStateProcess(state));
        }

        private IEnumerator ChangeStateProcess(ActionState state)
        {
            State = state;

            while (_cooldown > 0)
            {
                _cooldown -= Time.deltaTime;
                yield return null;
            }

            State = ActionState.Idle;
        }


        private bool IsAvailableToReload(IWeapon weapon)
        {
            //Mag is infinite
            if (weapon.Stats.InfiniteMag)
                return false;

            //All available ammo is already inside mag
            if (weapon.State.AmmoInMag == weapon.State.TotalAmmo)
                return false;

            //Mag is full
            if (weapon.State.AmmoInMag == weapon.Stats.MagSize)
                return false;

            return true;
        }
    }
}