using System.Linq;
using UnityEngine;
using Game.Plugins.Input;
using Game.Plugins.TimeProcesses;

namespace Game.Player.Components
{
    public class PlayerInput : PlayerUnitComponent
    {
        [SerializeField] private PlayerUnitActions _actions;

        private ITimeProcess _movementProcess;


        void OnEnable()
        {
            SubscribeToInput();
        }

        void OnDisable()
        {
            UnsubscribeFromInput();
        }


        void FixedUpdate()
        {
            ControlMovement();
        }



        private void ControlMovement()
        {
            _actions.ControlMovement(InputLocator.Service.MovementInput, InputLocator.Service.PointerWorldPosition);
        }


        private void OnShoot()
        {
            _actions.Shoot();
        }

        private void OnReload()
        {
            _actions.Reload();
        }

        private void OnWeaponDrop()
        {
            //TODO: Save starting weapon
            if (Weapons.Count <= 1)
                return;

            Inventory.DropItem(Weapons.Current.ID);
        }

        private void OnInteract()
        {
            if (Interactions.CurrentInteractable is null)
                return;

            Debug.Log($"Interacting with {Interactions.CurrentInteractable}");
            Interactions.CurrentInteractable.Interact(PlayerUnit);
        }


        private void OnNextWeapon()
        {
            _actions.SwitchWeaponToNext();
        }

        private void OnPreviousWeapon()
        {
            _actions.SwitchWeaponToPrevious();
        }


        private void SubscribeToInput()
        {
            InputLocator.Service.ShootInput.Pressed += OnShoot;
            InputLocator.Service.ReloadInput.Pressed += OnReload;

            InputLocator.Service.InteractInput.Pressed += OnInteract;
            InputLocator.Service.WeaponDropInput.Pressed += OnWeaponDrop;

            InputLocator.Service.NextWeaponInput += OnNextWeapon;
            InputLocator.Service.PreviousWeaponInput += OnPreviousWeapon;
        }

        private void UnsubscribeFromInput()
        {
            InputLocator.Service.ShootInput.Pressed -= OnShoot;
            InputLocator.Service.ReloadInput.Pressed -= OnReload;

            InputLocator.Service.InteractInput.Pressed -= OnInteract;
            InputLocator.Service.WeaponDropInput.Pressed -= OnWeaponDrop;

            InputLocator.Service.NextWeaponInput -= OnNextWeapon;
            InputLocator.Service.PreviousWeaponInput -= OnPreviousWeapon;
        }
    }
}