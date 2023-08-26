using System;
using Game.Units.Components;

namespace Game.Player.Components
{
    public static class PlayerDatabase
    {
        private static PlayerDatabaseSO _database;


        public static void Provide(PlayerDatabaseSO database)
        {
            _database = database;
        }


        public static Unit GetUnitPrefab(string id) => _database.GetCharacterData(id).PlayerUnitPrefab;
        public static PlayerStats GetDefaultStats(string id) => _database.GetCharacterData(id).GetDefaultStats();
    }
}