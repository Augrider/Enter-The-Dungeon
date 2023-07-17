using Game.Items.Components;

namespace Game.Plugins.ObjectPool
{
    public sealed class ItemsPool : ObjectPool<Item>
    {
        protected override void OnNewActivated(Item result)
        {
            result.ID = ItemDatabase.GetID(result);
            result.DefaultParent = transform;
        }
    }
}