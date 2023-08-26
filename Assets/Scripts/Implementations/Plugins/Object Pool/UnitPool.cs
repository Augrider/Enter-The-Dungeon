using Game.Units.Components;

namespace Game.Plugins.ObjectPool
{
    public class UnitPool : ObjectPool<Unit>
    {
        protected override void OnNewActivated(Unit result)
        {
        }
    }
}