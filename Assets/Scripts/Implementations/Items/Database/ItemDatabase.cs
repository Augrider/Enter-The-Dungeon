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


        public static Item GetPrefab(string id) => _database.GetPrefab(id);
    }
}