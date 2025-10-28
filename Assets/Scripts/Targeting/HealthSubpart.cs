using UnityEngine;

namespace Targeting {
    public class HealthSubpart : MonoBehaviour, ITarget {
        public Health Parent;
        public float DamageMultiplier;

        public void TakeDamage(float damage) {
            Parent.TakeDamage(damage * DamageMultiplier);
        }
    }
}