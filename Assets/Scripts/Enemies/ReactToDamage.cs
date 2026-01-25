using NUnit.Framework;
using Targeting;
using UnityEngine;
using Validation;

namespace Enemies {
    public class ReactToDamage : MonoBehaviour, IValidate {
        public Health health;
        public EnemyPlayerDetection detection;
        public int secondsInHighAlert;

        private void Start() {
            health.OnDamageTaken += _ => detection.ConsiderHighAlert(secondsInHighAlert);
        }

        public void Validate() {
            Assert.IsNotNull(health, "health != null");
            Assert.IsNotNull(detection, "detection != null");
            Assert.IsTrue(secondsInHighAlert >= 0, "secondsInHighAlert >= 0");
        }
    }
}