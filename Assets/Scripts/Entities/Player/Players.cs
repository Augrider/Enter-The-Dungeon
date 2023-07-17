namespace Game.Player
{
    public static class Players
    {
        public static IPlayer Current { get; private set; }
        public static bool IsCurrentAlive => Current != null ? Current.State.Health > 0 : false;


        public static void Provide(IPlayer player)
        {
            Current = player;
        }
    }
}