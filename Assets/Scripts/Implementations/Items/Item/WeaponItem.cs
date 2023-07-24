using Game.Player;
using Game.Weapons;

namespace Game.Items.Components
{
    public class WeaponItem : Item
    {
        private IWeapon _weapon;


        void Awake()
        {
            _weapon = GetComponent<IWeapon>();
        }


        public override void Interact(IPlayer player)
        {
            if (player.Weapons.TryGetWeapon(ID, out var weapon))
            {
                weapon.State.TotalAmmo = weapon.Stats.MaxAmmo;
                Destroy();
                return;
            }

            base.Interact(player);
        }


        protected override void OnItemPicked(IPlayer player)
        {
            TogglePhysics(false);
            _weapon.ToggleVisual(false);

            player.Weapons.AddWeapon(_weapon, ID);
        }

        protected override void OnItemDropped(IPlayer player)
        {
            player.Weapons.RemoveWeapon(_weapon);

            TogglePhysics(true);
            _weapon.ToggleVisual(true);
        }
    }
}