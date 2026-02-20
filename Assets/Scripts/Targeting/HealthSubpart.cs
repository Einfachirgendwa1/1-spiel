using NUnit.Framework;
using UnityEngine;
using Validation;

namespace Targeting {
    public class HealthSubpart : MonoBehaviour, IValidate {
        [NonNull] public AudioClip hurtSound;
        [PositiveNonZero] public float damageMultiplier = 1;

        public bool playDeathSound = true;
        public AudioClip deathSound;

        private Health parent;

        private void Start() {
            parent = FindParent(transform);
        }

        public void Validate() {
            Assert.IsNotNull(FindParent(transform), "FindParent(transform) != null");
            Assert.Equals(deathSound is not null, playDeathSound);
        }

        private static Health FindParent(Transform t) {
            while (true) {
                if (t.GetComponent<Health>() is { } health) return health;
                t = t.parent;
            }
        }

        public void TakeDamage(float damage) {
            parent.DamageTakenCallback();
            parent.health -= damage * damageMultiplier;
            if (parent.health <= 0) {
                if (playDeathSound) parent.audioSource.PlayOneShot(deathSound);
            } else {
                parent.audioSource.PlayOneShot(hurtSound, 100f);
            }
        }
    }
}