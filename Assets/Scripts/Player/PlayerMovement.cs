using System;
using System.Collections.Generic;
using Settings.Global.Input;
using UnityEngine;
using Action = Settings.Global.Input.Action;

namespace Player {
    public class PlayerMovement : MonoBehaviour {
        [Header("Required")] public Rigidbody rigidBody;
        [Header("Movement")] public float movementSpeed;
        public float movementAcceleration;
        public float jumpForce;

        private readonly List<GameObject> groundColliders = new();

        private float jumpBuffer;

        public void Update() {
            jumpBuffer = Math.Max(0, jumpBuffer - Time.deltaTime);

            if (Action.Jump.Is(Input.GetKeyDown)) {
                jumpBuffer = 0.5f;
            }
        }

        public void FixedUpdate() {
            if (groundColliders.Count > 0 && jumpBuffer > 0) {
                rigidBody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                jumpBuffer = 0;
            }

            Vector3 forward = Vector3.forward * Input.GetAxisRaw("Vertical");
            Vector3 right = Vector3.right * Input.GetAxisRaw("Horizontal");
            Vector3 direction = (transform.rotation * (forward + right)).normalized * movementSpeed;

            Vector3 movement = Vector3.Lerp(rigidBody.linearVelocity, direction, movementAcceleration);

            movement.y = rigidBody.linearVelocity.y;
            rigidBody.linearVelocity = movement;
        }

        private void OnCollisionEnter(Collision other) {
            Vector3 normal = other.contacts[0].normal;
            if (Vector3.Angle(normal, Vector3.up) < 60) {
                groundColliders.Add(other.gameObject);
            }
        }

        private void OnCollisionExit(Collision other) {
            groundColliders.Remove(other.gameObject);
        }
    }
}