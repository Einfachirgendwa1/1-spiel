using System.Collections;
using Targeting;
using UnityEngine;

namespace Enemies {
    public class ReactToDamage : MonoBehaviour {
        public Health health;
        public EnemyPlayerDetection detection;
        public int secondsInHighAlert;

        private void Start() {
            health.OnDamageTaken += _ => StartCoroutine(HighAlertCountdown());
        }

        private IEnumerator HighAlertCountdown() {
            detection.HighAlert = true;
            yield return new WaitForSeconds(secondsInHighAlert);
            detection.HighAlert = false;
        }
    }
}