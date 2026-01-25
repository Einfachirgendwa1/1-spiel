using Targeting;
using UnityEngine;

namespace Enemies {
    public class ReactToDamage : MonoBehaviour {
        public Health health;
        public EnemyPlayerDetection detection;
        public int secondsInHighAlert;

        private void Start() {
            health.OnDamageTaken += _ => detection.ConsiderHighAlert(secondsInHighAlert);
        }
    }
}