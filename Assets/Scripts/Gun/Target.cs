using UnityEngine;

namespace Gun {
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
                if (deathSound) {
                    audioSource?.PlayOneShot(deathSound);
                }

                Destroy(gameObject);
            } else if (hurtSound) {
                audioSource?.PlayOneShot(hurtSound);
            }
        }
    }
}