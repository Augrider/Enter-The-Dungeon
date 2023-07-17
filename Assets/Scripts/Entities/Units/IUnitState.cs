using UnityEngine;

namespace Game.Units
{
    public interface IUnitState
    {
        Vector3 Position { get; set; }

        Quaternion Rotation { get; set; }
        Vector3 Direction { get; set; }

        int Health { get; set; }
        int MaxHealth { get; set; }
    }
}