using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Plugins.Input
{
    public class InputData
    {
        public event Action Pressed;
        public event Action Released;

        public bool IsPressed { get; set; }


        public void TogglePressed(bool value)
        {
            if (value == IsPressed)
                return;

            IsPressed = value;

            if (value)
                Pressed?.Invoke();
            else
                Released?.Invoke();
        }
    }
}