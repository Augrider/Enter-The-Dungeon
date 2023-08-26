using UnityEngine;

namespace Game.Player.Components
{
    public abstract class PlayerUnitComponent : MonoBehaviour
    {
        [SerializeField] private PlayerUnit _playerUnit;
        public PlayerUnit PlayerUnit => _playerUnit;

        public IPlayerState PlayerState => PlayerUnit.State;

        public Interactions Interactions => PlayerUnit.Interactions;
        public PlayerUnitActions Actions => PlayerUnit.Actions;

        public ItemsInventory Inventory => PlayerUnit.Inventory;
        public WeaponsInventory Weapons => PlayerUnit.Weapons;
    }
}