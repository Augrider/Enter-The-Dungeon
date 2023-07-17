namespace Game.Enemies
{
    /// <summary>
    /// Handles enemy behavior, such as shooting and movement
    /// </summary>
    public interface IEnemyBehavior
    {
        bool Enabled { get; set; }
    }
}