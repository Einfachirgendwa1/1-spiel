using System;
using UnityEngine;
using Validation;

namespace Targeting {
    public class Health : MonoBehaviour, ITarget {
        [PositiveNonZero] public float health;
        [NonNull] public AudioClip hurtSound;
        [NonNull] public AudioClip deathSound;
        [NonNull] public AudioSource audioSource;

        public void TakeDamage(float damage) {
            OnDamageTaken?.Invoke(damage);

            health -= damage;

            if (health <= 0) {
                audioSource.PlayOneShot(deathSound);
                Destroy(gameObject);
            } else {
                audioSource.PlayOneShot(hurtSound);
            }
        }

        internal event Action<float> OnDamageTaken;
    }
}