using Targeting;
using UnityEngine;
using Validation;

namespace Enemies {
    public class AlertOnDamage : MonoBehaviour {
        [NonNull] public Health health;

        private float lastTimeDamaged = -Mathf.Infinity;

        private void Start() {
            health.OnDamageTaken += () => lastTimeDamaged = Time.time;
        }

        public float GetTimeSinceLastDamage() {
            return Time.time - lastTimeDamaged;
        }
    }
}