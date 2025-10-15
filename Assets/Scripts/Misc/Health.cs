using UnityEngine;

namespace Misc {
    public class Health : MonoBehaviour {
        public float health;
        public AudioClip hurtSound;
        public AudioClip deathSound;
        public AudioSource audioSource;

        public void TakeDamage(float damage) {
            health -= damage;

            if (health <= 0) {
                audioSource.PlayOneShot(deathSound);
                Destroy(gameObject);
            } else {
                audioSource.PlayOneShot(hurtSound);
            }
        }
    }
}