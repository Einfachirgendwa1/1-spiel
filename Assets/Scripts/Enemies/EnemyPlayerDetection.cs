using System;
using System.Collections;
using UnityEngine;

namespace Enemies {
    public enum EnemyState {
        Patrolling,
        Attacking
    }

    public class EnemyPlayerDetection : MonoBehaviour {
        public float radius;

        [Range(0, 360)] public float angle;

        public GameObject playerReference;

        public LayerMask targetMask;
        public LayerMask obstructionMask;

        [NonSerialized]
        public bool canSeePlayer;
        internal EnemyState state = EnemyState.Patrolling;

        private void Start() {
            playerReference = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine() {
            while (true) {
                yield return new WaitForSeconds(0.2f);
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck() {
            Collider[] rangeChecks = new Collider[1];
            if (Physics.OverlapSphereNonAlloc(transform.position, radius, rangeChecks, targetMask) == 0) {
                canSeePlayer = false;
                return;
            }

            Collider rangeCheck = rangeChecks[0];
            Transform target = rangeCheck.transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
            if (!(angleToTarget < angle / 2)) {
                return;
            }

            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            canSeePlayer = !Physics.Raycast(
                transform.position,
                directionToTarget,
                distanceToTarget,
                obstructionMask
            );
            state = canSeePlayer ? EnemyState.Attacking : EnemyState.Patrolling;

            if (canSeePlayer) {
                transform.LookAt(target);
            }
        }
    }
}