using UnityEngine;

namespace Game.Controller.Missions
{
    public class MissionObjective : MonoBehaviour
    {
        [SerializeField] private ObjectiveType _objectiveType;


        /// <summary>
        /// Tell the controller this objective was complete
        /// </summary>
        /// <remarks>
        /// Usually invoked by Unity Events, in inspector
        /// </remarks>
        public void Complete()
        {
            MissionController.CompleteObjective(_objectiveType);
        }
    }
}