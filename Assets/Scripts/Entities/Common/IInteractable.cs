using Game.Player;
using UnityEngine;

namespace Game.Common
{
    /// <summary>
    /// Interface for objects that player can interact with
    /// </summary>
    public interface IInteractable
    {
        bool Enabled { get; set; }

        Vector3 Position { get; }

        /// <summary>
        /// Toggle highlight to show interactable as target for interaction with player
        /// </summary>
        void ToggleHighlight(bool value);

        /// <summary>
        /// Perform intended interaction with player
        /// </summary>
        /// <param name="player">Player to interact with</param>
        void Interact(IPlayer player);
    }
}