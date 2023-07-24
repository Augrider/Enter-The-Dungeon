using UnityEngine;
using UnityEngine.Events;

namespace Game.Controller.Missions
{
    public class MissionController : MonoBehaviour
    {
        public static event System.Action<MissionController> MissionUpdated;

        [SerializeField] private UnityEvent _missionComplete;

        [SerializeField] private ObjectiveType _objectiveType;
        [SerializeField, Multiline] private string _description;
        [SerializeField] private int _objectiveCount;
        private int _completedCount = 0;

        public ObjectiveType ObjectiveType => _objectiveType;
        public string Description => _description;

        public int CompletedCount => _completedCount;
        public int MissionTarget => _objectiveCount;

        public bool Completed { get; private set; }


        void Awake()
        {
            MissionObjective.Completed += OnObjectiveCompleted;
        }

        //Used for initial status update
        private void Start()
        {
            MissionUpdated?.Invoke(this);
        }


        void OnDestroy()
        {
            MissionObjective.Completed -= OnObjectiveCompleted;
        }


        [ContextMenu("Complete Objective")]
        public void CompleteObjective() => OnObjectiveCompleted(_objectiveType);



        private void OnObjectiveCompleted(ObjectiveType objectiveType)
        {
            if (_objectiveType != objectiveType)
                return;

            _completedCount++;
            MissionUpdated?.Invoke(this);

            if (CompletedCount >= MissionTarget)
            {
                Completed = true;
                _missionComplete?.Invoke();
            }
        }
    }
}