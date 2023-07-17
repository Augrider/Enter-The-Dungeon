namespace Game.Common
{
    public interface IDestructible
    {
        bool Immune { get; }

        void DealDamage(int value);
    }
}