using Interaction;
using Settings.Input;
using TMPro;
using UnityEngine;

namespace UI {
    public class InteractHelp : MonoBehaviour {
        public TextMeshProUGUI interactText;

        private void Update() {
            interactText.text = PlayerInteraction.instance.CanInteract(out IInteractable interactable)
                ? $"Press {Keybinds.keybinds[Action.Interact]} to {interactable.Description}"
                : "";
        }
    }
}