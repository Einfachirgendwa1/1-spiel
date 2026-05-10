using System;
using System.Collections;
using Interaction;
using UnityEngine;
using Validation;

/*
 * X und Y kooordinaten sind wegen dem impot aus blender komischs
 */
namespace Lift {
    public class LiftButton : MonoBehaviour, IInteractable {
        public enum Mode {
            Low,
            High,
            Toggle
        }

        [NonNull] public Lift lift;
        [NonNull] public Transform liftTransform;
        [NonNull] public Mode mode;

        [SerializeField]
        public bool CanInteract =>
            mode switch {
                Mode.Toggle => IsUp() || IsDown(),
                Mode.High   => IsDown(),
                Mode.Low    => IsUp(),
                _           => throw new ArgumentOutOfRangeException()
            };

        public string Description =>
            mode switch {
                Mode.Toggle => $"move lift {(IsUp() ? "down" : "up")}",
                _           => "call lift"
            };

        public void Interact() {
            StartCoroutine(Move());
        }

        private bool IsUp() {
            return liftTransform.position.z >= lift.upPosY - lift.delta;
        }

        private bool IsDown() {
            return liftTransform.position.z <= lift.downPosY + lift.delta;
        }

        private IEnumerator Move() {
            if (CanInteract) {
                Vector3 direction = IsUp() ? Vector3.down : Vector3.up;

                while (direction == Vector3.down ? !IsDown() : !IsUp()) {
                    liftTransform.position += direction * lift.speed;
                    yield return new WaitForEndOfFrame();
                }
            }

            yield return null;
        }
    }
}