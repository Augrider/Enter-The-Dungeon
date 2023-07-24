using System;
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
        [SerializeField] private List<MissionUIData> _missions;


        private void OnEnable()
        {
            SubscribeToMissions();
        }

        private void OnDisable()
        {
            UnsubscribeFromMissions();
        }


        public void ResetMissions()
        {
            _missions.Clear();
            UpdateText();
        }



        private void SubscribeToMissions()
        {
            MissionController.MissionUpdated += OnMissionUpdated;
        }

        private void UnsubscribeFromMissions()
        {
            MissionController.MissionUpdated -= OnMissionUpdated;
        }


        private void OnMissionUpdated(MissionController controller)
        {
            Debug.LogWarning($"Mission {controller.ObjectiveType} updated");
            if (!_missions.Any(t => t.MissionType == controller.ObjectiveType))
            {
                var newData = GetNewMissionData(controller);
                _missions.Add(newData);
            }

            var missionData = _missions.First(t => t.MissionType == controller.ObjectiveType);
            missionData.CompletedCount = controller.CompletedCount;
            missionData.Completed = controller.Completed;

            UpdateText();
        }


        private void UpdateText()
        {
            var missionsText = "";

            foreach (var missionData in _missions)
            {
                var missionText = GetDescription(missionData);
                missionsText = string.Concat(missionsText, missionText, System.Environment.NewLine);
            }

            _missionTextObject.SetText(missionsText);
        }


        private static string GetDescription(MissionUIData missionData)
        {
            string missionText = "-" + missionData.Description;

            if (missionData.CompletedCount < missionData.MissionTarget)
                missionText += $" {missionData.CompletedCount}/{missionData.MissionTarget}";
            else
                missionText += " Completed";

            return missionText;
        }

        private static MissionUIData GetNewMissionData(MissionController controller)
        {
            return new MissionUIData()
            {
                MissionType = controller.ObjectiveType,
                Description = controller.Description,
                MissionTarget = controller.MissionTarget
            };
        }



        [System.Serializable]
        private class MissionUIData
        {
            public ObjectiveType MissionType;
            public string Description;

            public int CompletedCount;
            public int MissionTarget;

            public bool Completed;
        }
    }
}