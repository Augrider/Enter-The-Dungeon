using Game.Common;

namespace Game.Player
{
    public interface IPlayerInteractions
    {
        IInteractable CurrentInteractable { get; }


        bool TryInteractWith(IInteractable interactable);
        //When someone is near (in trigger):
        //Show tooltip for interaction (button, maybe, name and something else)

        //When button pressed and this can be interacted with:
        //Do contextual action (just send who interacts to subscribers)

        //Problems:
        //Information to show is dependant on item itself
        //How to actually show tooltips?
    }
}