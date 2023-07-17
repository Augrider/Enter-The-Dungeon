using UnityEngine;
using UnityEngine.Events;

namespace Game.Hacking
{
    /// <summary>
    /// This component initiates hacking game and translates results to objects
    /// </summary>
    public class HackingComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent HackSuccess;
        [SerializeField] private UnityEvent HackFailure;

        public static event System.Action<HackingComponent> HackStarted;


        public void Initiate()
        {
            HackStarted?.Invoke(this);
        }


        public void SetSuccess() => HackSuccess?.Invoke();
        public void SetFailed() => HackFailure?.Invoke();
    }
}