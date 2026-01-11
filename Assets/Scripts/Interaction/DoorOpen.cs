using UnityEngine;

namespace Interaction {
    public class DoorOpen : MonoBehaviour, IInteractable {
        [SerializeField] private bool isOpen = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start() { }

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