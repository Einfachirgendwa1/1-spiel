using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting;
using Validation;

namespace Enemies {
    public class Patrolling : MonoBehaviour {
        [NonNull] public NavMeshAgent agent;

        internal readonly List<Vector3> PatrollingPath = new();
        private int patrollingPathHead;
        private Vector3? target;

        private void Start() {
            if (PatrollingPath.Count != 0) {
                target = PatrollingPath[0];
            }
        }

        [Preserve]
        public void WhenPatrolling() {
            if (target == null) return;

            Vector3 distance = target.Value - transform.position;
            if (distance.magnitude < 0.5f) {
                patrollingPathHead = (patrollingPathHead + 1) % PatrollingPath.Count;
                target = PatrollingPath[patrollingPathHead];
            }

            agent.destination = target!.Value;
        }
    }
}