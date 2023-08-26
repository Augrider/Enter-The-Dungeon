using Game.Units;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayer
    {
        string CharacterID { get; }

        //Add position if needed, better compromise
        Vector3 Position { get; }

        PlayerStats Stats { get; set; }
        IPlayerState State { get; }

        IPlayerItems Inventory { get; }
        IPlayerWeapons Weapons { get; }

        void Import(PlayerFullData playerData);
        PlayerFullData Export();
    }
}