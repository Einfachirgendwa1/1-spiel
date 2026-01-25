using System;
using UnityEngine;
using Cursor = UI.Cursor;

namespace Enemies {
    public enum EnemyState {
        Patrolling,
        Alerted,
        Attacking
    }

    public class EnemyPlayerDetection : MonoBehaviour {
        public float radius;
        [Range(0, 360)] public float angle;
        public LayerMask obstructionMask;
        private readonly Cursor cursor = new();

        internal bool HighAlert;

        [NonSerialized] public GameObject Player;
        [NonSerialized] public EnemyState State = EnemyState.Patrolling;

        private void Start() {
            Player = GameObject.Find("/Player");
        }

        private void Update() {
            State = CanSeePlayer() ? EnemyState.Attacking : HighAlert ? EnemyState.Alerted : EnemyState.Patrolling;
            cursor.Str = State.ToString();
        }

        internal bool CanSeePlayer() {
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, Player.transform.position);
            float angleToTarget = Vector3.Angle(transform.forward, direction);

            bool obstructed = Physics.Raycast(
                transform.position,
                direction,
                distanceToTarget,
                obstructionMask
            );

            return distanceToTarget <= radius && angleToTarget <= angle / 2 && !obstructed;
        }
    }
}