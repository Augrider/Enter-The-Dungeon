using Game.Common;

namespace Game.Units
{
    public interface IUnit
    {
        /// <summary>
        /// Positional aspect of unit
        /// </summary>
        ITransformComponent Transform { get; }
        IDestructible Destructible { get; }

        // IUnitState State { get; }
    }
}