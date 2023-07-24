using System.Collections;
using System.Collections.Generic;
using Game.Player;
using UnityEngine;

namespace Game.UI.HUD
{
    public abstract class PlayerElement : MonoBehaviour
    {
        protected IPlayer Player { get; private set; }


        public void SetPlayer(IPlayer player)
        {
            Player = player;
            enabled = true;
        }

        public void ClearPlayer()
        {
            Player = null;
            enabled = false;
        }
    }
}