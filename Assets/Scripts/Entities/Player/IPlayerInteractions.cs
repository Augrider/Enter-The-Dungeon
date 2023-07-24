using Game.Common;

namespace Game.Player
{
    public interface IPlayerInteractions
    {
        IInteractable CurrentInteractable { get; }

        bool TryInteractWith(IInteractable interactable);
        //Remove from current interactables?
    }
}