using System;
using UnityEngine;
using Cursor = UI.Cursor;

namespace Enemies {
    public enum EnemyState {
        Patrolling,
        Attacking
    }

    public class EnemyPlayerDetection : MonoBehaviour {
        public float radius;
        [Range(0, 360)] public float angle;
        public LayerMask obstructionMask;

        private readonly Cursor cursor = new();

        [NonSerialized] public GameObject Player;
        [NonSerialized] public EnemyState State = EnemyState.Patrolling;

        private void Start() {
            Player = GameObject.Find("/Player");
        }

        private void Update() {
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, Player.transform.position);
            float angleToTarget = Vector3.Angle(transform.forward, direction);


            bool obstructed = Physics.Raycast(
                transform.position,
                direction,
                distanceToTarget,
                obstructionMask
            );

            bool canSeePlayer = distanceToTarget <= radius && angleToTarget <= angle / 2 && !obstructed;
            State = canSeePlayer ? EnemyState.Attacking : EnemyState.Patrolling;
            if (canSeePlayer) {
                transform.LookAt(Player.transform);
            }

            cursor.Str = State.ToString();
        }
    }
}