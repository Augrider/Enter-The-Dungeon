using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SaveSystem
{
    [System.Serializable]
    public struct WeaponData
    {
        //Ammo and some way to identify weapon. Additional database? Or item id can be used?
        //Weapons are usually received only as items. And even if something else gives them, effect still can give it as item
        public int ItemID;
        public int Ammo;
    }
}