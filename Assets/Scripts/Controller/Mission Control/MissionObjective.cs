using UnityEngine;

namespace Game.Controller.Missions
{
    internal class MissionObjective : MonoBehaviour
    {
        public static event System.Action<ObjectiveType> Completed;

        [SerializeField] private ObjectiveType _objectiveType;


        /// <summary>
        /// Tell the controller this objective was complete
        /// </summary>
        /// <remarks>
        /// Usually invoked by Unity Events, in inspector
        /// </remarks>
        public void Complete()
        {
            Completed?.Invoke(_objectiveType);
        }
    }
}