using Game.Player;

namespace Game.SaveSystem
{
    [System.Serializable]
    /// <summary>
    /// Contains all data that needs to be saved
    /// </summary>
    public struct SaveData
    {
        public int Level;
        public int EnemiesKilled;

        //If multiple players - multiple Player Data Save
        //Each player should have character prefab and other cosmetic data (name, mini icon...), referenced somehow
        //Naturally, reference should be placed inside Player Save Data, or stored here with it
        //If inside, there might be usage restrictions for PlayerSaveData type, too much info

        public PlayerFullData Player;


        public SaveData(int level, int enemiesKilled, PlayerFullData player)
        {
            Level = level;
            EnemiesKilled = enemiesKilled;
            Player = player;
        }
    }
}