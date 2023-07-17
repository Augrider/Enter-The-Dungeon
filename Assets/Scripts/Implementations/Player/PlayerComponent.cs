using Game.Units;
using UnityEngine;

namespace Game.Player.Components
{
    public abstract class PlayerComponent : MonoBehaviour
    {
        [SerializeField] private Player _player;

        public Player Player => _player;
        public IUnit Unit => _player.Unit;
    }
}