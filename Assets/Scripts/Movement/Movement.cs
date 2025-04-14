using UnityEngine;

namespace Movement {
    public abstract class Movement : MonoBehaviour {
        public float movementAcceleration = 20;
        public bool grounded = true;
        public new Rigidbody rigidbody;
        protected abstract LayerMask WhatIsGround { get; }

        public void FixedUpdate() {
            Vector3 plane = RecheckGrounded().normal;
            Vector3 src = rigidbody.linearVelocity;
            Vector3 dst = MovementDirection(plane);

            src.y = 0;
            dst.y = 0;
            Vector3 movement = MovementVector(src, dst);

            movement.y = rigidbody.linearVelocity.y;
            rigidbody.linearVelocity = movement;
        }

        private Vector3 MovementVector(Vector3 src, Vector3 dst) {
            return Vector3.Lerp(src, dst, movementAcceleration);
        }


        private RaycastHit RecheckGrounded() {
            grounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycasthit, 1.0F, WhatIsGround);
            return raycasthit;
        }

        protected abstract Vector3 MovementDirection(Vector3 plane);

        protected Vector3 Towards(Vector3 point) {
            return point - transform.position;
        }
    }
}