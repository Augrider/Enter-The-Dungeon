using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.State
{
    public sealed class PauseComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent Paused;
        [SerializeField] private UnityEvent Resumed;


        private void OnEnable()
        {
            GameStateLocator.Service.PauseSet += OnPauseSet;
        }

        private void OnDisable()
        {
            GameStateLocator.Service.PauseSet -= OnPauseSet;
        }


        public void TogglePause(bool value)
        {
            GameStateLocator.Service.Control.TogglePause(value);
        }

        public void SwitchPause()
        {
            GameStateLocator.Service.Control.TogglePause(!GameStateLocator.Service.Paused);
        }



        private void OnPauseSet(bool value)
        {
            if (value)
                Paused?.Invoke();
            else
                Resumed?.Invoke();
        }
    }
}