using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Common
{
    public class DestructibleComponent : MonoBehaviour, IDestructible
    {
        [SerializeField] private GameObject _object;

        [SerializeField] private int _health;
        [SerializeField] private float _immunityCooldown;

        [SerializeField] private UnityEvent<int> HealthChanged;
        [SerializeField] private UnityEvent ObjectDied;

        private bool _immune;

        public bool Immune => _immune;


        public void DealDamage(int value)
        {
            _health -= value;
            HealthChanged?.Invoke(_health);

            if (_health <= 0)
            {
                ObjectDied?.Invoke();
                Destroy(_object);
                return;
            }

            if (_immunityCooldown > 0)
                StartCoroutine(ImmunityProcess());
        }



        private IEnumerator ImmunityProcess()
        {
            _immune = true;

            yield return new WaitForSeconds(_immunityCooldown);

            _immune = false;
        }
    }
}