using System.Diagnostics.CodeAnalysis;
using Settings.Global.Input;
using UnityEngine;
using Action = Settings.Global.Input.Action;

namespace Interaction {
    public class PlayerInteraction : MonoBehaviour {
        internal static PlayerInteraction Instance;

        public Camera playerCamera;
        public float distance;
        public LayerMask playerLayer;

        private void Start() {
            Instance = this;
        }


        private void Update() {
            if (Action.Interact.Is(Input.GetKeyDown) && CanInteract(out IInteractable interactable)) {
                interactable.Interact();
            }
        }

        internal bool CanInteract([NotNullWhen(true)] out IInteractable interactable) {
            interactable = null;

            Vector3 origin = playerCamera.transform.position;
            Vector3 direction = playerCamera.transform.forward;

            bool hit = Physics.Raycast(origin, direction, out RaycastHit interaction, distance, ~playerLayer);
            bool component = hit && interaction.collider.gameObject.TryGetComponent(out interactable);
            return component && interactable.CanInteract;
        }
    }
}