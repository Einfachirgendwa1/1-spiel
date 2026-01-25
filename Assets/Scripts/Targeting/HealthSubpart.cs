using UnityEngine;
using Validation;

namespace Targeting {
    public class HealthSubpart : MonoBehaviour, ITarget {
        [NonNull] public Health parent;

        [PositiveNonZero] public float damageMultiplier;

        public void TakeDamage(float damage) {
            parent.TakeDamage(damage * damageMultiplier);
        }
    }
}