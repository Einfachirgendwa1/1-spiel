using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies {
    [Serializable]
    public class EnemyMovement : MonoBehaviour {
        public NavMeshAgent agent;
        public EnemyPlayerDetection detection;
        internal List<Vector3> PatrollingPath = new();
        private int patrollingPathHead;
        private Vector3? target;

        private void Start() {
            if (PatrollingPath.Count != 0) {
                target = PatrollingPath[0];
            }
        }

        private void Update() {
            agent.destination = detection.State switch {
                EnemyState.Patrolling => FollowPatrollingPath(),
                EnemyState.Attacking  => AttackPlayer(),
                _                     => throw new ArgumentOutOfRangeException()
            };
        }

        private Vector3 FollowPatrollingPath() {
            // if we have no points to go to, so we just stand there doing nothing
            if (target == null) {
                return transform.position;
            }

            Vector3 distance = target.Value - transform.position;
            if (distance.magnitude < 0.5f) {
                patrollingPathHead = (patrollingPathHead + 1) % PatrollingPath.Count;
                target = PatrollingPath[patrollingPathHead];
            }

            return target!.Value;
        }

        private Vector3 AttackPlayer() {
            Vector3 playerPosition = detection.Player.transform.position;
            Vector3 distance = playerPosition - transform.position;
            float distanceToPlayer = distance.magnitude;

            return distanceToPlayer switch {
                > 4 => playerPosition,
                _   => transform.position
            };
        }
    }
}