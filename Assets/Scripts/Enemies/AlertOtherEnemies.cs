using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Enemies {
    public class AlertOtherEnemies : MonoBehaviour {
        public EnemyPlayerDetection detection;
        public LayerMask enemyLayer;
        public float alertRadius;
        public float alertDelaySeconds;

        [CanBeNull] private IEnumerator routine;

        private void Update() {
            if (detection.State == EnemyState.Attacking && routine is null) {
                routine = AlertOthers();
                StartCoroutine(routine);
            }
        }

        private IEnumerator AlertOthers() {
            yield return new WaitForSeconds(alertDelaySeconds);

            Collider[] colliders = new Collider[100];
            int size = Physics.OverlapSphereNonAlloc(transform.position, alertRadius, colliders, enemyLayer);

            for (int x = 0; x < size; x++) {
                if (colliders[x].GetComponentInChildren<EnemyPlayerDetection>() is { } otherDetection) {
                    otherDetection.ConsiderHighAlert(5);
                }
            }
        }
    }
}