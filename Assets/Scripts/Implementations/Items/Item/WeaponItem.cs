using Game.Player;
using Game.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Items.Components
{
    public class WeaponItem : Item
    {
        [SerializeField] private IWeapon _weapon;


        void Awake()
        {
            _weapon = GetComponent<IWeapon>();
        }


        public override void Interact(IPlayer player)
        {
            if (player.Weapons.TryGetWeapon(ID, out var weapon))
            {
                Debug.Log($"Same weapon {this} detected, refilling");

                weapon.State.TotalAmmo = weapon.Stats.MaxAmmo;
                Destroy();
                return;
            }

            base.Interact(player);
        }


        protected override void OnItemPicked(IPlayer player)
        {
            Debug.Log($"Picking weapon {this}");
            TogglePhysics(false);
            ToggleVisual(false);

            player.Weapons.AddWeapon(_weapon);
        }

        protected override void OnItemDropped(IPlayer player)
        {
            player.Weapons.RemoveWeapon(_weapon);

            TogglePhysics(true);
            ToggleVisual(true);
        }
    }
}