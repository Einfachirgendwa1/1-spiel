using JetBrains.Annotations;
using UnityEngine;

namespace Gun {
    public class Health : MonoBehaviour {
        public float health;
        [CanBeNull] public AudioClip hurtSound;
        [CanBeNull] public AudioClip deathSound;
        [CanBeNull] public AudioSource audioSource;

        public void TakeDamage(float damage) {
            health -= damage;

            if (health <= 0) {
                if (deathSound != null) {
                    audioSource?.PlayOneShot(deathSound);
                }

                Destroy(gameObject);
            } else if (hurtSound != null) {
                audioSource?.PlayOneShot(hurtSound);
            }
        }
    }
}