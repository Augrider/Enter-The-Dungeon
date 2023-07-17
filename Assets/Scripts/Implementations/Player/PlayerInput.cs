using UnityEngine;
using Game.Plugins.Input;
using Game.Units.Components;

namespace Game.Player.Components
{
    public class PlayerInput : PlayerComponent
    {
        //Input first
        [SerializeField] private RigidbodyMovementComponent _movementComponent;


        void OnEnable()
        {
            InputLocator.Service.ShootInput.Pressed += OnShoot;
            InputLocator.Service.ReloadInput.Pressed += OnReload;

            InputLocator.Service.InteractInput.Pressed += OnInteract;
            InputLocator.Service.WeaponDropInput.Pressed += OnWeaponDrop;

            InputLocator.Service.NextWeaponInput += OnNextWeapon;
            InputLocator.Service.PreviousWeaponInput += OnPreviousWeapon;
        }

        void OnDisable()
        {
            InputLocator.Service.ShootInput.Pressed -= OnShoot;
            InputLocator.Service.ReloadInput.Pressed -= OnReload;

            InputLocator.Service.InteractInput.Pressed -= OnInteract;
            InputLocator.Service.WeaponDropInput.Pressed -= OnWeaponDrop;

            InputLocator.Service.NextWeaponInput -= OnNextWeapon;
            InputLocator.Service.PreviousWeaponInput -= OnPreviousWeapon;
        }

        void FixedUpdate()
        {
            ControlMovement();
        }



        private void ControlMovement()
        {
            _movementComponent.RotateTowards(InputLocator.Service.PointerWorldPosition);

            if (InputLocator.Service.MovementInput.sqrMagnitude > 0)
            {
                var movementInput = new Vector3(InputLocator.Service.MovementInput.x, 0, InputLocator.Service.MovementInput.y);
                _movementComponent.SetSpeed(Player.Stats.Speed * movementInput);
            }
        }



        private void OnShoot()
        {
            Player.Weapons.Shoot();
        }

        private void OnReload()
        {
            Player.Weapons.Reload();
        }

        private void OnWeaponDrop()
        {
            if (Player.Weapons.TryGetID(Player.Weapons.Current, out var itemID))
                Player.Inventory.DropItem(itemID);
        }

        private void OnInteract()
        {
            if (Player.Interactions.CurrentInteractable != null)
                Player.Interactions.TryInteractWith(Player.Interactions.CurrentInteractable);
        }


        private void OnNextWeapon()
        {
            Player.Weapons.SwitchToNext();
        }

        private void OnPreviousWeapon()
        {
            Player.Weapons.SwitchToPrevious();
        }
    }
}