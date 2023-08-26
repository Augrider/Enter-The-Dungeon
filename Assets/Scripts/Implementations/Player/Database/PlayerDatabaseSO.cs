using System.Linq;
using Developed.Database;
using UnityEngine;

namespace Game.Player.Components
{
    [CreateAssetMenu(menuName = "Database/Players")]
    public class PlayerDatabaseSO : BaseScriptableDatabase<PlayerData>
    {
        public PlayerData GetCharacterData(string id)
        {
            try
            {
                return _database.First(t => t.ID == id).Item;
            }
            catch
            {
                Debug.LogError($"Character {id} was not found!");
                return null;
            }
        }


        protected override void SetID(PlayerData item, string value)
        {
            item.ID = value;
            UnityEditor.EditorUtility.SetDirty(this);
            // item.PlayerUnitPrefab.ID
        }
    }
}