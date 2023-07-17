using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// Contains stats (character and weapon modifiers) and state of a player
    /// </summary>
    public interface IPlayerState
    {
        PlayerStats Stats { get; set; }

        int Health { get; set; }
    }
}