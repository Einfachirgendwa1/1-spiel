using System;
using UnityEngine;

namespace Enemies {
    public class Path {
        private int index;
        private readonly Vector3[] points;

        public Path(Vector3[] points) {
            this.points = points;
            index = 0;
        }

        public Vector3? NextPoint() {
            if (points.Length == 0) {
                return null;
            }

            index = index++ % points.Length;
            return points[index];
        }
    }

    public class EnemyMovement : Movement.Movement {
        public int acceleration = 20;

        public LayerMask whatIsGround;

        private readonly Path patrollingPath = new(new Vector3[] { });

        private EnemyPlayerDetection detection;
        private Vector3? target;
        protected override LayerMask WhatIsGround => whatIsGround;

        private void Start() {
            detection = GetComponent<EnemyPlayerDetection>();
            rigidbody = GetComponent<Rigidbody>();
            movementAcceleration = acceleration;
        }

        private Vector3 FollowPath(Path path) {
            target ??= path.NextPoint();
            if (target == null) {
                return Vector3.zero;
            }

            Vector3 distance = target.Value - transform.position;
            if (distance.magnitude < 0.1f) {
                target = path.NextPoint();
            }

            return Vector3.forward;
        }

        protected override Vector3 MovementDirection(Vector3 plane) {
            return detection.state switch {
                EnemyState.Patrolling => FollowPath(patrollingPath),
                EnemyState.Attacking => AttackPlayer(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private Vector3 AttackPlayer() {
            // Transform playerTransform = detection.playerReference.transform;
            return Vector3.back;
        }
    }
}