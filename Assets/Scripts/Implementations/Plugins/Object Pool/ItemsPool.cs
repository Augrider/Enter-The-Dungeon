using Game.Items.Components;

namespace Game.Plugins.ObjectPool
{
    public sealed class ItemsPool : ObjectPool<Item>
    {
        void Awake()
        {
            Item.DefaultParent = transform;
        }

        void OnDestroy()
        {
            Item.DefaultParent = null;
        }


        protected override void OnNewActivated(Item result)
        {
            result.enabled = true;
        }
    }
}