using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Common
{
    public interface ITransformComponent
    {
        Transform MainTransform { get; }

        Vector3 Position { get; set; }

        Quaternion Rotation { get; set; }
        Vector3 Direction { get; set; }
    }
}