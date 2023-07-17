using System.Collections;
using System.Collections.Generic;
using Game.Player;
using UnityEngine;

namespace Game.Items.Components
{
    public class StatsChangeItem : Item
    {
        [SerializeField] private PlayerStats _statsDelta;


        protected override void OnItemPicked(IPlayer player)
        {
            Debug.Log("Stats changed");
            player.Stats += _statsDelta;
        }

        protected override void OnItemDropped(IPlayer player)
        {
            Debug.Log("Stats changed");
            player.Stats -= _statsDelta;
        }
    }
}