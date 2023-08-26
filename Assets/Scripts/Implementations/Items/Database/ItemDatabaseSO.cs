using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Developed.Database;
using Game.Common;

namespace Game.Items.Components
{
    [CreateAssetMenu(menuName = "Database/Items")]
    public class ItemDatabaseSO : BaseScriptableDatabase<Item>
    {
        public Item GetPrefab(string id)
        {
            var record = _database.First(t => t.ID == id);
            return record.Item;
        }


        protected override void SetID(Item item, string value)
        {
            var idComponent = item.GetComponent<IDComponent>();
            idComponent.SetID(value);
        }
    }
}