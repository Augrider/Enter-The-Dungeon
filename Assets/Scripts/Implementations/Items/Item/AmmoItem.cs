using System.Collections;
using System.Collections.Generic;
using Game.Player;
using UnityEngine;

namespace Game.Items.Components
{
    public class AmmoItem : Item
    {
        [SerializeField] private int ammoRestore;


        protected override void OnItemPicked(IPlayer player)
        {
            Debug.Log("Ammo restored");
            player.Weapons.Current.State.TotalAmmo += ammoRestore;
        }

        protected override void OnItemDropped(IPlayer player)
        {
            Debug.Log("Ammo deducted");
            player.Weapons.Current.State.TotalAmmo -= ammoRestore;
        }
    }
}