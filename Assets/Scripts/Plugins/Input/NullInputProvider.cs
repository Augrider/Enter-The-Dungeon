using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Plugins.Input
{
    public class NullInputProvider : IInputProvider
    {
        public Vector3 PointerWorldPosition => throw new NotImplementedException();

        public Vector2 MovementInput => throw new NotImplementedException();


        public InputData ShootInput => throw new NotImplementedException();
        public InputData ReloadInput => throw new NotImplementedException();
        public InputData RollInput => throw new NotImplementedException();
        public InputData InteractInput => throw new NotImplementedException();

        public InputData WeaponDropInput => throw new NotImplementedException();

        public event Action NextWeaponInput;
        public event Action PreviousWeaponInput;
    }
}