using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Developed.Database
{
    public abstract class BaseScriptableDatabase<T> : ScriptableObject, IDatabaseEditorHandler where T : UnityEngine.Object
    {
        [SerializeField, HideInInspector] protected ItemRecord[] _database;


#if UNITY_EDITOR
        [SerializeField] private List<T> _items;

        void OnValidate()
        {
            var handler = this as IDatabaseEditorHandler;
            handler.UpdateDatabase();
        }
#endif


        void IDatabaseEditorHandler.UpdateDatabase()
        {
#if UNITY_EDITOR
            var itemList = new List<T>(_items.Count);

            //Get all valid items
            foreach (var item in _items.Distinct())
            {
                if (item is null)
                    continue;

                itemList.Add(item);
            }

            _database = itemList.Select(t => new ItemRecord(GenerateID(t), t)).ToArray();

            //Save all changes
            foreach (var record in _database)
                SetID(record.Item, record.ID);

            EditorUtility.SetDirty(this);
#endif
        }

        string IDatabaseEditorHandler.GetContents()
        {
            var result = $"Item Database {name}: Elements Count = {_database.Length};\n";

            foreach (var record in _database)
            {
                result += $"\n   [{record.ID}, {record.Item}]";
            }

            return result;
        }


        protected virtual string GenerateID(T item) => item.name;
        protected abstract void SetID(T item, string value);



        //TODO: Change to record later
        [System.Serializable]
        protected readonly struct ItemRecord
        {
            public readonly string ID;
            public readonly T Item;


            public ItemRecord(string id, T item)
            {
                ID = id;
                Item = item;
            }
        }
    }
}