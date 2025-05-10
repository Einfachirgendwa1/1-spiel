using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies {
    [Serializable]
    public class EnemyMovement : MonoBehaviour {
        private NavMeshAgent agent;
        private EnemyPlayerDetection detection;
        private List<Vector3> patrollingPath = new();
        private int patrollingPathHead;
        private Vector3? target;

        private void Start() {
            detection = GetComponent<EnemyPlayerDetection>();
            agent = GetComponent<NavMeshAgent>();

            Transform child = transform.Find("Path");
            if (child) {
                foreach (Transform child2 in child) {
                    patrollingPath.Add(child2.position);
                }
            }

            if (patrollingPath.Count != 0) {
                target = patrollingPath[0];
            }
        }

        private void Update() {
            agent.destination = detection.state switch {
                EnemyState.Patrolling => FollowPatrollingPath(),
                EnemyState.Attacking  => AttackPlayer(),
                _                     => throw new ArgumentOutOfRangeException()
            };
        }

        private Vector3 FollowPatrollingPath() {
            // we have no points to go to, so we just stand there doing nothing
            if (target == null) {
                return transform.position;
            }

            Vector3 distance = target.Value - transform.position;
            if (distance.magnitude < 0.5f) {
                patrollingPathHead = (patrollingPathHead + 1) % patrollingPath.Count;
                target = patrollingPath[patrollingPathHead];
            }

            return target!.Value;
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