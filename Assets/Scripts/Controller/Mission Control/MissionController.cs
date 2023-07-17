using UnityEngine;
using UnityEngine.Events;

namespace Game.Controller.Missions
{
    public class MissionController : MonoBehaviour
    {
        public static event System.Action<ObjectiveType> ObjectiveUpdated;
        public event System.Action MissionUpdated;
        [SerializeField] private UnityEvent _missionComplete;

        [SerializeField] private ObjectiveType _objectiveType;
        [SerializeField] private int _objectiveCount;
        private int _completedCount = 0;

        public bool Completed { get; private set; }
        public int MissionTarget => _objectiveCount;
        public int CompletedCount => _completedCount;


        void Awake()
        {
            ObjectiveUpdated += OnObjectiveCompleted;
        }

        void OnDestroy()
        {
            ObjectiveUpdated -= OnObjectiveCompleted;
        }


        [ContextMenu("Complete Objective")]
        public void CompleteObjective() => CompleteObjective(_objectiveType);
        public static void CompleteObjective(ObjectiveType objectiveType) => ObjectiveUpdated?.Invoke(objectiveType);



        private void OnObjectiveCompleted(ObjectiveType objectiveType)
        {
            if (_objectiveType != objectiveType)
                return;

            _completedCount++;
            MissionUpdated?.Invoke();

            if (CompletedCount >= MissionTarget)
            {
                Completed = true;
                _missionComplete?.Invoke();
            }
        }
    }
}