namespace Game.Weapons.Components
{
    public class SingleShotLauncher : BaseLauncher
    {
        protected override void OnShoot(float deviation)
        {
            var direction = GetDeviatedDirection(transform.forward, deviation);

            CreateProjectile(transform.position, direction);
        }
    }
}