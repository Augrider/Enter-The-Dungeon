using System.Collections;
using UnityEngine;

namespace Game.Units.Components
{
    public class NavMeshMovementComponent : MonoBehaviour
    {
        // public bool IsMoving { get; private set; }
        // public event System.Action Stopped;

        [SerializeField] private UnityEngine.AI.NavMeshAgent _navMeshAgent;


        private void OnEnable()
        {
            StartCoroutine(FixSpawnProcess());
        }

        private void OnDisable()
        {
            _navMeshAgent.enabled = false;
        }


        public void SetDestination(Vector3 target)
        {
            if (!_navMeshAgent.enabled || !_navMeshAgent.isOnNavMesh)
                return;

            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(target);
        }

        public void Stop()
        {
            if (!_navMeshAgent.enabled || !_navMeshAgent.isOnNavMesh)
                return;

            _navMeshAgent.SetDestination(transform.position);
            _navMeshAgent.isStopped = true;
        }


        /// <summary>
        /// Fixes navMesh issues with spawning
        /// </summary>
        private IEnumerator FixSpawnProcess()
        {
            yield return null;

            _navMeshAgent.enabled = true;
            _navMeshAgent.Warp(transform.position);
        }
    }
}