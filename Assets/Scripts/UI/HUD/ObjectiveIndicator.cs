using System.Collections.Generic;
using System.Linq;
using Game.Controller.Missions;
using TMPro;
using UnityEngine;

namespace Game.UI.HUD
{
    public class ObjectiveIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _missionTextObject;
        [SerializeField] private MissionUIData[] _missions;


        private void OnEnable()
        {
            SubscribeToMissions(_missions.Select(t => t.Mission).AsEnumerable());
            UpdateText();
        }

        private void OnDisable()
        {
            UnsubscribeFromMissions(_missions.Select(t => t.Mission).AsEnumerable());
        }



        private void UpdateText()
        {
            var missionsText = "";

            foreach (var missionData in _missions)
            {
                var missionText = "";

                if (missionData.Mission.CompletedCount < missionData.Mission.MissionTarget)
                    missionText = string.Format(missionData.MissionText, $"{missionData.Mission.CompletedCount}/{missionData.Mission.MissionTarget}");
                else
                    missionText = string.Format(missionData.MissionText, "Completed");

                Debug.Log(missionText);
                missionsText = string.Concat(missionsText, missionText, System.Environment.NewLine);
            }

            _missionTextObject.SetText(missionsText);
        }


        private void SubscribeToMissions(IEnumerable<MissionController> missions)
        {
            foreach (var mission in missions)
                mission.MissionUpdated += UpdateText;
        }

        private void UnsubscribeFromMissions(IEnumerable<MissionController> missions)
        {
            foreach (var mission in missions)
                mission.MissionUpdated -= UpdateText;
        }



        [System.Serializable]
        private struct MissionUIData
        {
            public MissionController Mission;
            [Multiline] public string MissionText;
        }
    }
}