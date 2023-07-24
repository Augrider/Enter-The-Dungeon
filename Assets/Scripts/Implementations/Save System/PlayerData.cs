using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SaveSystem
{
    [System.Serializable]
    public struct PlayerData
    {
        //Currency, items, weapon states
        public int Level;

        public int Health;

        public int Currency;

        public int[] Items;
        public WeaponData[] WeaponData;
        public int EnemiesKilled;
    }
}