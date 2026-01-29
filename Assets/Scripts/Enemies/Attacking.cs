using System;
using Guns;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting;
using Validation;

namespace Enemies {
    public class Attacking : MonoBehaviour {
        [NonNull] public NavMeshAgent agent;
        [NonNull] public EnemyGunController gunController;

        public LayerMask obstructionMask;

        [PositiveNonZero] public float radius;
        [Range(0, 360)] public float angle;


        [NonSerialized] public GameObject Player;


        private void Start() => Player = GameObject.Find("/Player");

        [Preserve]
        public void WhenAttacking() {
            Vector3 playerPosition = Player.transform.position;
            Vector3 distance = playerPosition - transform.position;
            float distanceToPlayer = distance.magnitude;

            agent.destination = distanceToPlayer switch {
                > 4 => playerPosition,
                _   => transform.position
            };

            transform.LookAt(Player.transform);
            gunController.InputBuffer[State.Shoot] = 1;
        }

        public bool CanSeePlayer() {
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, Player.transform.position);
            float angleToTarget = Vector3.Angle(transform.forward, direction);

            bool obstructed = Physics.Raycast(
                transform.position,
                direction,
                distanceToTarget,
                obstructionMask
            );

            float r = radius;
            return distanceToTarget <= r && angleToTarget <= angle / 2 && !obstructed;
        }
    }
}