using Settings;
using UnityEngine;

namespace Player {
    internal interface IInteractable {
        public void Interact();
    }

    public class PlayerInteraction : MonoBehaviour {
        public Camera playerCamera;
        public float distance = 2f;
        public LayerMask playerLayer;

        private void Update() {
            if (Action.Interact.Is(Input.GetKeyDown)) {
                Vector3 origin = playerCamera.transform.position;
                Vector3 direction = playerCamera.transform.forward;

                bool hit = Physics.Raycast(origin, direction, out RaycastHit interaction, distance, ~playerLayer);

                if (hit && interaction.collider.gameObject.TryGetComponent(out IInteractable interactable)) {
                    interactable.Interact();
                }
            }
        }
    }
}