using Settings.Input;
using UnityEngine;

namespace Player {
    public class PlayerMovement : MonoBehaviour {
        [Header("Required")] public Rigidbody rigidBody;
        public LayerMask groundLayerMask;
        [Header("Movement")] public float movementSpeed = 20;
        public float movementAcceleration = 20;
        public float jumpForce = 4;

        private bool grounded;

        public void Update() {
            if (Action.Jump.Is(Input.GetKeyDown) && grounded) {
                rigidBody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                grounded = false;
            }
        }

        public void FixedUpdate() {
            grounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.0F, groundLayerMask);

            Vector3 plane = hit.normal;
            Vector3 src = rigidBody.linearVelocity;
            Vector3 dst = MovementDirection(plane);

            src.y = 0;
            dst.y = 0;
            Vector3 movement = Vector3.Lerp(src, dst, movementAcceleration);

            movement.y = rigidBody.linearVelocity.y;
            rigidBody.linearVelocity = movement;
        }


        private Vector3 MovementDirection(Vector3 plane) {
            Vector3 moveDirection = Vector3.forward * Input.GetAxisRaw("Vertical") +
                                    Vector3.right * Input.GetAxisRaw("Horizontal");
            Vector3 direction = (transform.rotation * moveDirection).normalized;
            return Vector3.ProjectOnPlane(direction, plane) * movementSpeed;
        }
    }
}