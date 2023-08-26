using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Common
{
    /// <summary>
    /// Special component used for storing identification number and sharing it between components
    /// </summary>
    public sealed class IDComponent : MonoBehaviour
    {
        [SerializeField] private string _ID;
        public string Value => _ID;


        public void SetID(string value)
        {
            _ID = value;
        }
    }
}