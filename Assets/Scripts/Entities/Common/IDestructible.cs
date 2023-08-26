using System;

namespace Game.Common
{
    public interface IDestructible
    {
        event Action<int> HealthChanged;

        int Health { get; set; }
        int MaxHealth { get; set; }

        bool Immune { get; }

        void ReceiveDamage(int value);
    }
}