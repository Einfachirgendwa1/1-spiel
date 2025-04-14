using System;
using UnityEngine;

namespace Enemies {
    public class Path {
        private Vector3[] points;
        private int index;

        public Path(Vector3[] points) {
            this.points = points;
            this.index = 0;
        }

        public Vector3? NextPoint() {
            if (points.Length == 0) return null;
            index = index++ % points.Length;
            return points[index];
        }
    }
    
    public class EnemyMovement : Movement.Movement {
        public int acceleration = 20;
        
        private readonly Path patrollingPath = new Path(new Vector3[] {});
        private Vector3? target;
        
        private EnemyPlayerDetection detection;
        
        public LayerMask whatIsGround;
        protected override LayerMask WhatIsGround => whatIsGround;

        private void Start() {
            detection = GetComponent<EnemyPlayerDetection>();
            rigidbody = GetComponent<Rigidbody>();
            movementAcceleration = acceleration;
        }

        private Vector3 FollowPath(Path path) {
            target ??= path.NextPoint();
            if (target == null) return Vector3.zero;
            
            Vector3 distance = target.Value - transform.position;
            if (distance.magnitude < 0.1f) {
                target = path.NextPoint();
            }

            return Vector3.forward;
        }

        protected override Vector3 MovementDirection(Vector3 plane) {
            switch (detection.state) {
                case EnemyState.Patrolling:
                    return FollowPath(patrollingPath);
                case EnemyState.Attacking:
                    return AttackPlayer();
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private Vector3 AttackPlayer() {
            // Transform playerTransform = detection.playerReference.transform;
            return Vector3.back;
        }
    }
}