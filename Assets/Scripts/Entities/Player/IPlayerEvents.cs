using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayerEvents
    {
        event Action<int> HealthChanged;

        event Action InventoryChanged;

        //Should weapon and abilities events be here?
        event Action WeaponFired;
        event Action WeaponSwitched;
        event Action WeaponReloaded;
    }
}