using UnityEngine;
using Validation;

namespace Targeting {
    public class HealthSubpart : MonoBehaviour, ITarget {
        [NonNull] public Health parent;
        [NonNull] public AudioClip hurtSound;

        [PositiveNonZero] public float damageMultiplier = 1;

        public void TakeDamage(float damage) {
            Debug.Log("headshot");
            parent.TakeDamage(damage * damageMultiplier);
            parent.audioSource.PlayOneShot(hurtSound);
        }
    }
}