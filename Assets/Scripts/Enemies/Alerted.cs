using System;
using System.Collections;
using Targeting;
using UnityEngine;
using UnityEngine.Scripting;
using Validation;

namespace Enemies {
    public class Alerted : MonoBehaviour {
        public LayerMask enemyLayer;
        [PositiveNonZero] public float alertOnDamageDuration;
        [PositiveNonZero] public float alertOthersRadius;
        [PositiveNonZero] public float alertOthersDelaySeconds;

        [NonNull] public Health health;
        [NonNull] public EnemyMovement movement;
        [NonNull] public ListenForGunSounds listener;
        public Animator enemyAnim;

        private bool alertingOthers;
        private float duration;

        private void Start() {
            enemyAnim = GetComponent<Animator>();
            health.OnDamageTaken += () => { duration = Math.Max(duration, alertOnDamageDuration); };

        }

        private void Update()
        {
            duration = Mathf.Max(0f, duration - Time.deltaTime);
            enemyAnim.SetBool("isAiming", IsAlerted());
        }

        public void Alert(float length) {
            duration = Mathf.Max(duration, length);
        }

        [Preserve]
        public bool IsAlerted() => duration > 0f;

        [Preserve]
        public void WhenAlerted() {
            if (!alertingOthers) StartCoroutine(AlertOthers());

            transform.Rotate(Vector3.up, Mathf.Sin(2 * Time.time) / 4);
            movement.Destination = listener.TargetPosition ?? transform.position;
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