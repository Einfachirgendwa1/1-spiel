using UnityEngine;

namespace Interaction {
    public class DoorOpen : MonoBehaviour, IInteractable {
        public bool isOpen;

        public string Description => "move door";

        public bool CanInteract => true;

        public void Interact() {
            if (!isOpen) {
                transform.Rotate(0, 0, 90);
                isOpen = true;
            } else {
                transform.Rotate(0, 0, -90);
                isOpen = false;
            }
        }
    }
}