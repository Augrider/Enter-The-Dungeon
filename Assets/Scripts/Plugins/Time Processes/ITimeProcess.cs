namespace Game.Plugins.TimeProcesses
{
    public interface ITimeProcess
    {
        bool InProgress { get; }

        void Stop();
    }
}