using Game.Player;
using UnityEngine;

namespace Game.Items.Components
{
    public class HealthItem : Item
    {
        [SerializeField] private int healthRestore;


        protected override void OnItemPicked(IPlayer player)
        {
            Debug.Log("Health restored");
            player.State.Health += healthRestore;
        }

        protected override void OnItemDropped(IPlayer player)
        {
            Debug.Log("Health deducted");
            player.State.Health -= healthRestore;
        }
    }
}