using UnityEngine;

namespace Game.Units
{
    public interface IUnitState
    {
        int Health { get; set; }
        int MaxHealth { get; set; }
    }
}