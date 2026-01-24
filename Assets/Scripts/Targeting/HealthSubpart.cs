using UnityEngine;
using UnityEngine.Serialization;

namespace Targeting {
    public class HealthSubpart : MonoBehaviour, ITarget {
        [FormerlySerializedAs("Parent")] public Health parent;

        [FormerlySerializedAs("DamageMultiplier")]
        public float damageMultiplier;

        public void TakeDamage(float damage) {
            parent.TakeDamage(damage * damageMultiplier);
        }
    }
}