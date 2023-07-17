namespace Game.Weapons
{
    public interface IWeaponState
    {
        int AmmoInMag { get; set; }
        int TotalAmmo { get; set; }

        float RecoilPercentage { get; set; }
        float Deviation { get; set; }

        WeaponStats Stats { get; }

        void ApplyStatsChange(WeaponStats stats);
    }
}