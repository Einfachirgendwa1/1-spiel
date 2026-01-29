using System;
using System.Collections;
using Targeting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting;
using Validation;

namespace Enemies {
    public class Alerted : MonoBehaviour {
        public LayerMask enemyLayer;
        [PositiveNonZero] public float alertOnDamageDuration;
        [PositiveNonZero] public float alertOthersRadius;
        [PositiveNonZero] public float alertOthersDelaySeconds;

        [NonNull] public Health health;
        [NonNull] public NavMeshAgent agent;

        private bool alertingOthers;
        private float duration;


        private void Start() => health.OnDamageTaken += () => {
            duration = Math.Max(duration, alertOnDamageDuration);
            Debug.Log($"Took damage, now alerted for {duration} seconds.");
        };

        private void Update() => duration = Mathf.Max(0f, duration - Time.deltaTime);

        [Preserve]
        public bool IsAlerted() => duration > 0f;

        [Preserve]
        public void WhenAlerted() {
            if (!alertingOthers) StartCoroutine(AlertOthers());

            transform.Rotate(Vector3.up, 5);
            agent.destination = transform.position;
        }

        private IEnumerator AlertOthers() {
            alertingOthers = true;

            yield return new WaitForSeconds(alertOthersDelaySeconds);

            Collider[] colliders = new Collider[100];
            int size = Physics.OverlapSphereNonAlloc(transform.position, alertOthersRadius, colliders, enemyLayer);

            for (int x = 0; x < size; x++) {
                if (colliders[x].GetComponentInChildren<Alerted>() is { } otherAlerted) {
                    otherAlerted.duration = Math.Max(duration, 5);
                }
            }

            alertingOthers = false;
        }
    }
}