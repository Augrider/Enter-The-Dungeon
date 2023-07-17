using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons.Components
{
    public class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        public Weapon Weapon => _weapon;
        public IWeaponState State => _weapon.State;
    }
}