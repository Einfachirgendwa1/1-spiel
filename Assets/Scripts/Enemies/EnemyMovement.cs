using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies {

    [Serializable]
    public class EnemyMovement : MonoBehaviour {
        private NavMeshAgent agent;
        private EnemyPlayerDetection detection;
        private Transform[] patrollingPath;
        private int patrollingPathHead;
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

            agent.destination = detection.state switch {
                EnemyState.Patrolling => FollowPatrollingPath(),

                EnemyState.Attacking  => AttackPlayer(),
                _                     => throw new ArgumentOutOfRangeException()
            };
        }


        private Vector3 FollowPath() {
            return Vector3.zero;

        private Vector3 FollowPatrollingPath() {
            // target ??= path.NextPoint();

            // we have no points to go to, so we just stand there doing nothing
            if (target == null) {
                return transform.position;
            }

            Vector3 distance = target.Value - transform.position;
            if (distance.magnitude < 0.1f) {
                // target = path.NextPoint();
                Assert.IsTrue(target.HasValue);
            }

            return target.Value;
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