using Interaction;
using Settings.Global.Input;
using UI.Text;
using UnityEngine;

namespace UI {
    public class InteractText : ElementDisplay {
        private void Update() {
            bool canInteract = PlayerInteraction
                               .Instance.CanInteract(out IInteractable interactable)
                               .Then(() =>
                                   SetElements(
                                       new ElementBuilder(Keybinds.Keybindings[Action.Interact]),
                                       new ElementBuilder(" to "),
                                       new ElementBuilder(Extensions.Rgba(30, 30, 30, 255), interactable.Description) {
                                           BackgroundSizeDelta = new Vector2(20, 10)
                                       }
                                   )
                               );

            SetVisible(canInteract);
        }
    }
}