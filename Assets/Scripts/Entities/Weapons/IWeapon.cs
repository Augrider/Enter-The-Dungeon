namespace Game.Weapons
{
    public interface IWeapon
    {
        IWeaponState State { get; }
        WeaponStats Stats { get; }

        void ToggleVisual(bool value);
        //Add callback function? Or just cooldowns to switching?
        void PullOut();
        void Hide();

        bool Shoot();
        void Reload();
    }
}