using Interaction;
using Settings.Input;
using UI.Text;
using UnityEngine;

namespace UI {
    public class InteractText : ElementDisplay {
        private void Update() {
            bool canInteract = PlayerInteraction
                .instance.CanInteract(out IInteractable interactable)
                .Then(() =>
                    SetElements(
                        new ElementBuilder(Keybinds.keybinds[Action.Interact]),
                        new ElementBuilder(" to "),
                        new ElementBuilder(Extensions.Rgba(30, 30, 30, 255), interactable.Description) {
                            backgroundSizeDelta = new Vector2(20, 10)
                        }
                    )
                );

            SetVisible(canInteract);
        }
    }
}