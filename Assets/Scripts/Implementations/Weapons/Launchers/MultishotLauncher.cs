using UnityEngine;

namespace Game.Weapons.Components
{
    public class MultishotLauncher : BaseLauncher
    {
        [Space]
        [SerializeField, Min(2)] private int _shotsAmount = 2;
        [SerializeField] private bool _evenSpread;
        [SerializeField] private float _spreadAngle;


        protected override void OnShoot(float deviation)
        {
            if (_evenSpread)
                CreateEvenSpread(deviation);
            else
                CreateRandomSpread(deviation);
        }



        private void CreateRandomSpread(float deviationAngle)
        {
            for (int i = 0; i < _shotsAmount; i++)
            {
                var maxDeviation = _spreadAngle * (i + 1) / _shotsAmount;
                var deviation = deviationAngle + Random.Range(-maxDeviation, maxDeviation);

                var direction = GetDeviatedDirection(transform.forward, deviation);

                CreateProjectile(transform.position, direction);
            }
        }

        private void CreateEvenSpread(float deviationAngle)
        {
            var spreadCone = _spreadAngle * 2;
            var shotDeviation = spreadCone / (_shotsAmount - 1);

            for (int i = 0; i < _shotsAmount; i++)
            {
                var direction = GetDeviatedDirection(transform.forward, deviationAngle - _spreadAngle + i * shotDeviation);

                CreateProjectile(transform.position, direction);
            }
        }
    }
}