using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Enemies {
    [Serializable]
    public class Path {
        private readonly Vector3[] points;
        private int index;

        public Path(Vector3[] points) {
            this.points = points;
            index = 0;
        }

        /// <summary>
        ///     Returns the next point in the path.
        /// </summary>
        /// <returns>Next point in the path or null if the path has no points.</returns>
        public Vector3? NextPoint() {
            if (points.Length == 0) {
                return null;
            }

            index = index++ % points.Length;
            return points[index];
        }
    }

    public class EnemyMovement : Movement.Movement {
        public LayerMask whatIsGround;
        public Path patrollingPath;

        private EnemyPlayerDetection detection;

        private Vector3? target;
        protected override LayerMask WhatIsGround => whatIsGround;

        private void Start() {
            detection = GetComponent<EnemyPlayerDetection>();
            rigidbody = GetComponent<Rigidbody>();
        }

        private Vector3 FollowPath(Path path) {
            target ??= path.NextPoint();

            // we have no points to go to, so we just stand there doing nothing
            if (target == null) {
                return Vector3.zero;
            }

            Vector3 distance = target.Value - transform.position;
            if (distance.magnitude < 0.1f) {
                target = path.NextPoint();
                Assert.IsTrue(target.HasValue);
            }

            return Towards(target.Value);
        }

        protected override Vector3 MovementDirection(Vector3 plane) {
            return detection.state switch {
                EnemyState.Patrolling => FollowPath(patrollingPath),
                EnemyState.Attacking  => AttackPlayer(),
                _                     => throw new ArgumentOutOfRangeException()
            };
        }

        private Vector3 AttackPlayer() {
            Vector3 playerPosition = detection.playerReference.transform.position;
            Vector3 distance = playerPosition - transform.position;
            float distanceToPlayer = distance.magnitude;

            return distanceToPlayer switch {
                > 7.5F => Towards(playerPosition),
                _      => Vector3.zero
            };
        }
    }
}