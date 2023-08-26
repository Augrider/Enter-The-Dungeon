using Game.Common;

namespace Game.Player
{
    public interface IPlayerInteractions
    {
        bool TryInteractWithCurrent();
        bool TryInteractWith(IInteractable interactable);
    }
}