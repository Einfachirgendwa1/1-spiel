using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Settings.Global.Input;
using UnityEngine;
using Validation;
using Action = Settings.Global.Input.Action;

namespace Interaction {
    public class PlayerInteraction : MonoBehaviour, IValidate {
        internal static PlayerInteraction Instance;

        [NonNull] public Camera playerCamera;
        [PositiveNonZero] public float distance;
        public LayerMask playerLayer;

        private void Start() {
            Instance = this;
        }


        private void Update() {
            if (Action.Interact.Is(Input.GetKeyDown) && CanInteract(out IInteractable interactable)) {
                interactable.Interact();
            }
        }

        public void Validate() {
            Assert.IsTrue((playerLayer.value & 1 << gameObject.layer) != 0, "player is in playerLayer");
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