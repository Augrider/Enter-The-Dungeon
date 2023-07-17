using UnityEngine;
using UnityEngine.Events;

namespace Game.Map.Components
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Collider _barrierCollider;

        [SerializeField] private bool _locked;
        [SerializeField] private bool _opened;

        [SerializeField] private UnityEvent _doorLocked;
        [SerializeField] private UnityEvent _doorUnlocked;

        [SerializeField] private UnityEvent _doorOpened;
        [SerializeField] private UnityEvent _doorClosed;


        void Start()
        {
            if (_locked)
            {
                _locked = false;
                ToggleLocked(true);
                return;
            }

            if (_opened)
            {
                _opened = false;
                Open();
                return;
            }
        }


        public void Open()
        {
            if (_locked || _opened)
                return;

            _opened = true;
            _barrierCollider.enabled = false;

            _doorOpened?.Invoke();
        }

        public void Close()
        {
            if (!_opened)
                return;

            _opened = false;
            _barrierCollider.enabled = true;

            _doorClosed?.Invoke();
        }

        //TODO: Remember if it was opened before locking
        public void ToggleLocked(bool value)
        {
            if (_locked == value)
                return;

            _locked = value;

            if (_locked)
            {
                _doorLocked?.Invoke();
                Close();
            }
            else
                _doorUnlocked?.Invoke();
        }
    }
}