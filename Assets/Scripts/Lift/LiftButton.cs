using System;
using System.Collections;
using Interaction;
using UnityEngine;

namespace Lift {
    public class LiftButton : MonoBehaviour, IInteractable {
        public enum Mode {
            Low,
            High,
            Toggle
        }

        public Lift lift;
        public Transform liftTransform;
        public Mode mode;

        public bool CanInteract =>
            mode switch {
                Mode.Toggle => isUp() || isDown(),
                Mode.High   => isDown(),
                Mode.Low    => isUp(),
                _           => throw new ArgumentOutOfRangeException()
            };

        public string Description =>
            mode switch {
                Mode.Toggle => $"move lift {(isUp() ? "down" : "up")}",
                _           => "call lift"
            };

        public void Interact() {
            StartCoroutine(Move());
        }

        private bool isUp() {
            return liftTransform.position.y >= lift.upPosY - lift.delta;
        }

        private bool isDown() {
            return liftTransform.position.y <= lift.downPosY + lift.delta;
        }

        private IEnumerator Move() {
            if (CanInteract) {
                Vector3 direction = isUp() ? Vector3.down : Vector3.up;

                while (direction == Vector3.down ? !isDown() : !isUp()) {
                    liftTransform.position += direction * lift.speed;
                    yield return new WaitForEndOfFrame();
                }
            }

            yield return null;
        }
    }
}