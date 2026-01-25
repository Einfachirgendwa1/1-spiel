using System;
using NUnit.Framework;
using UnityEngine;
using Validation;

namespace Enemies {
    public enum EnemyState {
        Patrolling,
        Alerted,
        Attacking
    }

    public class EnemyPlayerDetection : MonoBehaviour, IValidate {
        [PositiveNonZero] public float radius;
        [PositiveNonZero] public float radiusWhenAlerted;

        [UnityEngine.Range(0, 360)] public float angle;

        public LayerMask obstructionMask;

        private float highAlertTimerSecs;

        [NonSerialized] public GameObject Player;
        [NonSerialized] public EnemyState State = EnemyState.Patrolling;

        private void Start() {
            Player = GameObject.Find("/Player");
        }

        private void Update() {
            highAlertTimerSecs -= Time.deltaTime;

            bool highAlert = highAlertTimerSecs > 0;
            State = CanSeePlayer() ? EnemyState.Attacking : highAlert ? EnemyState.Alerted : EnemyState.Patrolling;
        }

        public void Validate() {
            Assert.IsTrue(radiusWhenAlerted >= radius, "radiusWhenAlerted >= radius");
        }

        internal void ConsiderHighAlert(float seconds) {
            highAlertTimerSecs = Math.Max(highAlertTimerSecs, seconds);
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