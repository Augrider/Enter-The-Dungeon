using Game.Units;
using UnityEngine;

namespace Game.Player.Components
{
    public abstract class PlayerComponent
    {
        public Player Player { get; internal set; }


        public PlayerComponent(Player player)
        {
            SetPlayer(player);
        }

        internal void SetPlayer(Player player)
        {
            Player = player;
        }
    }
}