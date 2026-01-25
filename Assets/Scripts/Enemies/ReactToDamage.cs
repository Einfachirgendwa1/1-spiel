using System.Collections;
using JetBrains.Annotations;
using Targeting;
using UnityEngine;

namespace Enemies {
    public class ReactToDamage : MonoBehaviour {
        public Health health;
        public EnemyPlayerDetection detection;
        public int secondsInHighAlert;

        [CanBeNull] private IEnumerator routine;

        private void Start() {
            health.OnDamageTaken += _ => {
                if (routine != null) {
                    StopCoroutine(routine);
                }

                routine = HighAlertCountdown();
                StartCoroutine(routine);
            };
        }

        private IEnumerator HighAlertCountdown() {
            detection.HighAlert = true;
            yield return new WaitForSeconds(secondsInHighAlert);
            detection.HighAlert = false;
        }
    }
}