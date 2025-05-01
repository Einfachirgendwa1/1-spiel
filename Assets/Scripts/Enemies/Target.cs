using UnityEngine;

namespace Enemies {
    public class Target : MonoBehaviour {
        public float health;
        public AudioClip hurtSound;
        public AudioClip deathSound;

        private AudioSource audioSource;

        private void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        public void TakeDamage(float damage) {
            health -= damage;

            if (health <= 0) {
                if (deathSound != null) {
                    audioSource.PlayOneShot(deathSound);
                }

                Destroy(gameObject);
            } else if (hurtSound != null) {
                audioSource.PlayOneShot(hurtSound);
            }
        }
    }
}