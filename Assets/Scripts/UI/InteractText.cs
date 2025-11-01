using Interaction;
using Settings.Input;
using UI.Text;
using Color = UnityEngine.Color;

namespace UI {
    public class InteractText : ElementDisplay {
        private void Update() {
            bool canInteract = PlayerInteraction.instance.CanInteract(out IInteractable interactable);

            SetVisible(canInteract);
            if (canInteract) {
                SetElements(
                    new ElementBuilder(Keybinds.keybinds[Action.Interact]),
                    new ElementBuilder(" to "),
                    new ElementBuilder(new Color(0.1f, 0.1f, 0.1f, 0.9f), interactable.Description)
                );
            }
        }
    }
}