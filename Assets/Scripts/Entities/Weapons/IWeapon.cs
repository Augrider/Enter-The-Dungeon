namespace Game.Weapons
{
    public interface IWeapon
    {
        string ID { get; }

        WeaponStats Stats { get; }
        IWeaponState State { get; }

        void PullOut();
        void Hide();

        bool Shoot();
        void Reload();
    }
}