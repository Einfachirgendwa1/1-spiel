using System;
using UnityEngine;

namespace Targeting {
    public class Health : MonoBehaviour, ITarget {
        public float health;
        public AudioClip hurtSound;
        public AudioClip deathSound;
        public AudioSource audioSource;

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