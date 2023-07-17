using Game.Units;

namespace Game.Player
{
    public interface IPlayer
    {
        bool InputEnabled { get; set; }

        //Add references to Stats and State
        IUnit Unit { get; }
        IUnitState UnitState { get; }

        IPlayerEvents Events { get; }

        PlayerStats Stats { get; set; }
        IPlayerState State { get; }

        IPlayerInteractions Interactions { get; }
        IPlayerItems Inventory { get; }
        IPlayerWeapons Weapons { get; }
    }
}