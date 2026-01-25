using System;
using UnityEngine;

namespace Enemies {
    public enum EnemyState {
        Patrolling,
        Alerted,
        Attacking
    }

    public class EnemyPlayerDetection : MonoBehaviour {
        public float radius;
        public float radiusWhenAlerted;
        [Range(0, 360)] public float angle;
        public LayerMask obstructionMask;

        internal float HighAlertTimerSecs;

        [NonSerialized] public GameObject Player;
        [NonSerialized] public EnemyState State = EnemyState.Patrolling;

        private void Start() {
            Player = GameObject.Find("/Player");
        }

        private void Update() {
            HighAlertTimerSecs -= Time.deltaTime;

            bool highAlert = HighAlertTimerSecs > 0;
            State = CanSeePlayer() ? EnemyState.Attacking : highAlert ? EnemyState.Alerted : EnemyState.Patrolling;
        }

        internal void ConsiderHighAlert(float seconds) {
            HighAlertTimerSecs = Math.Max(HighAlertTimerSecs, seconds);
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

            float r = State == EnemyState.Patrolling ? radius : radiusWhenAlerted;
            return distanceToTarget <= r && angleToTarget <= angle / 2 && !obstructed;
        }
    }
}