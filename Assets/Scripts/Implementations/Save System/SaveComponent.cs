using UnityEngine;

namespace Game.SaveSystem
{
    public class SaveComponent : MonoBehaviour
    {
        [SerializeField] private string fileName;


        // [ContextMenu("Save player")]
        public void Save(SaveData playerData)
        {
            var dataString = JsonUtility.ToJson(playerData);
            Debug.Log(dataString);

            System.IO.File.WriteAllText(GetFilePath(fileName), dataString);
        }

        [ContextMenu("Load player")]
        public SaveData Load()
        {
            if (!IsSaved())
                return new SaveData();

            var dataString = System.IO.File.ReadAllText(GetFilePath(fileName));
            Debug.Log(dataString);
            var playerData = JsonUtility.FromJson<SaveData>(dataString);

            return playerData;
        }

        public bool IsSaved()
        {
            if (!System.IO.File.Exists(GetFilePath(fileName)))
                return false;

            var dataString = System.IO.File.ReadAllText(GetFilePath(fileName));
            return !string.IsNullOrEmpty(dataString);
        }

        [ContextMenu("Erase save")]
        public void ClearSave()
        {
            if (!IsSaved())
                return;

            System.IO.File.Delete(GetFilePath(fileName));
        }



        private static string GetFilePath(string fileName)
        {
            return System.IO.Path.Combine(Application.persistentDataPath, fileName);
        }
    }
}