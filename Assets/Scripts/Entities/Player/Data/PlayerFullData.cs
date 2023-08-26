namespace Game.Player
{
    [System.Serializable]
    /// <summary>
    /// Contains all player data and state, exported into structure
    /// </summary>
    public struct PlayerFullData
    {
        public string CharacterID;
        public PlayerStateData State;

        public PlayerFullData(string characterID, PlayerStateData state)
        {
            CharacterID = characterID;
            State = state;
        }
    }
}