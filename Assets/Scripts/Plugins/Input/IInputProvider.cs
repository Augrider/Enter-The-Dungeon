using System;
using UnityEngine;

namespace Game.Plugins.Input
{
    public interface IInputProvider
    {
        InputData InteractInput { get; }

        InputData ShootInput { get; }
        InputData ReloadInput { get; }

        InputData RollInput { get; }
        InputData WeaponDropInput { get; }

        event Action NextWeaponInput;
        event Action PreviousWeaponInput;

        Vector3 PointerWorldPosition { get; }

        Vector2 MovementInput { get; }
    }
}