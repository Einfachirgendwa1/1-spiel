using System;
using Settings.Input;
using UnityEngine;
using Action = Settings.Input.Action;

namespace Player {
    public class PlayerMovement : MonoBehaviour {
        [Header("Required")] public Rigidbody rigidBody;
        public LayerMask groundLayerMask;
        [Header("Movement")] public float movementSpeed;
        public float movementAcceleration;
        public float jumpForce;

        private bool grounded;
        private float jumpBuffer;

        public void Update() {
            jumpBuffer = Math.Max(0, jumpBuffer - Time.deltaTime);

            if (Action.Jump.Is(Input.GetKeyDown)) {
                jumpBuffer = 0.5f;
            }
        }

        public void FixedUpdate() {
            grounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.0F, groundLayerMask);

            if (grounded && jumpBuffer > 0) {
                rigidBody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                grounded = false;
            }

            Vector3 plane = grounded ? hit.normal : Vector3.up;
            Vector3 current = rigidBody.linearVelocity;
            Vector3 target = MovementDirection(plane);

            Vector3 movement = Vector3.Lerp(current, target, movementAcceleration);

            movement.y = rigidBody.linearVelocity.y;
            rigidBody.linearVelocity = movement;
        }


        private Vector3 MovementDirection(Vector3 plane) {
            Vector3 forward = Vector3.forward * Input.GetAxisRaw("Vertical");
            Vector3 right = Vector3.right * Input.GetAxisRaw("Horizontal");

            Vector3 direction = (transform.rotation * (forward + right)).normalized;
            return Vector3.ProjectOnPlane(direction, plane) * movementSpeed;
        }
    }
}