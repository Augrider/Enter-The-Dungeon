namespace Game.Enemies
{
    /// <summary>
    /// Handles enemy movement behavior (wandering, etc)
    /// </summary>
    public interface IMovementBehavior
    {
        void Play();
        void Stop();

        void GoNext();
    }
}