using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items.Components
{
    //TODO: Make database actually read items from XML file
    public static class ItemDatabase
    {
        private static ItemDatabaseSO _database;


        public static void Provide(ItemDatabaseSO database)
        {
            _database = database;
        }


        // public static int IndexOf(IItem item) => _database.IndexOf(item);
        public static int GetID(Item item) => _database.IndexOf(item);
        public static Item GetPrefab(int index) => _database.GetPrefab(index);
    }
}