using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies {
    public class EnemyMovement : MonoBehaviour {
        private NavMeshAgent agent;
        private EnemyPlayerDetection detection;
        private Vector3? target;

        private void Start() {
            detection = GetComponent<EnemyPlayerDetection>();
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update() {
            agent.destination = GetTargetPoint();
        }

        private Vector3 GetTargetPoint() {
            return detection.state switch {
                EnemyState.Patrolling => FollowPath(),
                EnemyState.Attacking  => AttackPlayer(),
                _                     => throw new ArgumentOutOfRangeException()
            };
        }

        private Vector3 FollowPath() {
            return Vector3.zero;
        }

        private Vector3 AttackPlayer() {
            Vector3 playerPosition = detection.playerReference.transform.position;
            Vector3 distance = playerPosition - transform.position;
            float distanceToPlayer = distance.magnitude;

            return distanceToPlayer switch {
                > 4 => playerPosition,
                _   => transform.position
            };
        }
    }
}