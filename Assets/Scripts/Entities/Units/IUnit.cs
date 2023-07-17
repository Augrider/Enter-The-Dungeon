namespace Game.Units
{
    public interface IUnit
    {
        /// <summary>
        /// This event is fired when state of the unit is changed (excluding position and rotation)
        /// </summary>
        event System.Action StateChanged;

        IUnitState State { get; }
    }
}