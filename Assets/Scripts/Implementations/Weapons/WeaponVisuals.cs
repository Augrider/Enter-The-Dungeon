using System;
using UnityEngine;

namespace Game.Weapons.Components
{
    public class WeaponVisuals : MonoBehaviour
    {
        [SerializeField] private GameObject _visuals;


        //Animations and sound
        internal void ToggleEnabled(bool value)
        {
            _visuals.SetActive(value);
        }
    }
}