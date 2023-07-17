using UnityEngine;

namespace Game.Weapons.Components
{
    public class RecoilComponent : WeaponComponent
    {
        [Header("Deviation parameters")]
        [Space]
        [SerializeField] private float _maxDeviation;

        [Header("Recoil parameters")]
        [Space]
        [SerializeField] private float _shotRecoil;
        [SerializeField] private float _recoilControlMultiplier;

        private float _recoilState = 0;
        private float _deviationState = 0;


        void LateUpdate()
        {
            var recoilControl = _recoilControlMultiplier * Time.deltaTime;

            if (_recoilState > 0.01f)
                SetDeviationState(_deviationState - _deviationState * recoilControl / _recoilState);
            else
                SetDeviationState(0);

            SetRecoilPercentage(_recoilState - recoilControl);
        }


        //Deviation and recoil effects are calculated here
        public void OnShoot()
        {
            var maxDeviation = _maxDeviation * _recoilState;

            SetDeviationState(_deviationState - Random.Range(0, maxDeviation) * Mathf.Sign(_deviationState));
            SetRecoilPercentage(_recoilState + _shotRecoil);
        }

        public void Reset()
        {
            SetDeviationState(0);
            SetRecoilPercentage(0);
        }



        private void SetRecoilPercentage(float value)
        {
            if (_recoilState == value)
                return;

            _recoilState = Mathf.Clamp(value, 0, 1);
            State.RecoilPercentage = _recoilState;
        }

        private void SetDeviationState(float value)
        {
            if (_deviationState == value)
                return;

            _deviationState = Mathf.Clamp(value, -_maxDeviation, _maxDeviation);
            State.Deviation = _deviationState;
        }
    }
}