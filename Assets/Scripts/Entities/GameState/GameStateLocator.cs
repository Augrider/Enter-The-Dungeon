namespace Game.State
{
    public static class GameStateLocator
    {
        private static IGameState NullService { get; } = new NullGameState();
        public static IGameState Service { get; private set; } = NullService;


        public static void Provide(IGameState value)
        {
            Service = value != null ? value : NullService;
        }
    }
}