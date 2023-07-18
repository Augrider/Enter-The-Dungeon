using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Plugins.Input
{
    public class InputPresenter : MonoBehaviour, IInputProvider
    {
        [SerializeField] private LayerMask _floorMask;

        public event Action NextWeaponInput;
        public event Action PreviousWeaponInput;

        public Vector3 PointerWorldPosition { get; private set; }

        public Vector2 MovementInput { get; private set; }

        public InputData InteractInput { get; } = new InputData();

        public InputData ShootInput { get; } = new InputData();
        public InputData ReloadInput { get; } = new InputData();

        public InputData RollInput { get; } = new InputData();
        public InputData WeaponDropInput { get; } = new InputData();


        void Awake()
        {
            InputLocator.Provide(this);
        }

        void OnDestroy()
        {
            InputLocator.Provide(null);
        }


        private void Update()
        {
            SetPointer(Mouse.current.position.ReadValue());
        }


        public void SetPointer(Vector2 screenPosition)
        {
            try
            {
                var screenRay = Camera.main.ScreenPointToRay(screenPosition);
                var hit = Physics.Raycast(screenRay, out RaycastHit hitInfo, 1000, _floorMask);

                if (hit)
                {
                    PointerWorldPosition = hitInfo.point;
                }
            }
            catch
            {
                Debug.LogWarning("No camera for raycast found!");
            }
        }


        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
        }


        public void OnShoot(InputAction.CallbackContext context)
        {
            ProcessInput(ShootInput, context);
        }

        public void OnReload(InputAction.CallbackContext context)
        {
            ProcessInput(ReloadInput, context);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            ProcessInput(InteractInput, context);
        }

        public void OnWeaponDrop(InputAction.CallbackContext context)
        {
            ProcessInput(WeaponDropInput, context);
        }

        public void OnWeaponSwitch(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            var input = context.ReadValue<Vector2>();

            if (input.y > 0)
                NextWeaponInput?.Invoke();
            if (input.y < 0)
                PreviousWeaponInput?.Invoke();
        }



        private void ProcessInput(InputData inputData, InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                inputData.TogglePressed(true);
                return;
            }

            if (context.canceled)
                inputData.TogglePressed(false);
        }
    }
}