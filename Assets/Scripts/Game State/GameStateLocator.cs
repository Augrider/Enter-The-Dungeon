namespace Game.State
{
    public static class GameStateLocator
    {
        public static IGameState Service { get; private set; }


        public static void Provide(IGameState value)
        {
            Service = value;
        }
    }
}