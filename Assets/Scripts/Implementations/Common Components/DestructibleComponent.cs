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
        [SerializeField] private UnityEvent<int> ReceivedDamage;
        [SerializeField] private UnityEvent ObjectDied;

        private bool _immune;

        public bool Immune => _immune;


        public void SetHealth(int value)
        {
            // if (_health == value)
            //     return;

            _health = value;
            HealthChanged?.Invoke(_health);

            if (_health <= 0)
            {
                ObjectDied?.Invoke();
                _object.SetActive(false);
                return;
            }
        }

        public void DealDamage(int value)
        {
            SetHealth(_health - value);
            ReceivedDamage?.Invoke(value);

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