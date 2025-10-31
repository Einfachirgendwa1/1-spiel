using Interaction;
using Settings.Input;
using UI.Text;

namespace UI {
    public class InteractText : TextDisplay {
        private void Update() {
            bool canInteract = PlayerInteraction.instance.CanInteract(out IInteractable interactable);

            SetVisible(canInteract);
            if (canInteract) {
                SetDisplayElements(Keybinds.keybinds[Action.Interact], " to ", interactable.Description);
            }
        }
    }
}