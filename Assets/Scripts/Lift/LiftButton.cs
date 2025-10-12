using System.Collections;
using Player;
using UnityEngine;

namespace Lift {
    public class LiftButton : MonoBehaviour, IInteractable {
        public Lift lift;

        public void Interact() {
            StartCoroutine(Move());
        }

        private bool isUp() {
            return transform.position.y >= lift.upPosY - lift.delta;
        }

        private bool isDown() {
            return transform.position.y <= lift.downPosY + lift.delta;
        }

        private IEnumerator Move() {
            if (isUp() || isDown()) {
                Vector3 direction = isUp() ? Vector3.down : Vector3.up;

                while (direction == Vector3.down ? !isDown() : !isUp()) {
                    transform.position += direction * lift.speed;
                    yield return new WaitForEndOfFrame();
                }
            }

            yield return null;
        }
    }
}