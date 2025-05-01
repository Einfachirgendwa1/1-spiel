using UnityEngine;

namespace Enemies {
    public class Target : MonoBehaviour {
        [SerializeField] private float health = 100f;
        [SerializeField] private AudioClip hurtSound;
        [SerializeField] private AudioClip deathSound;

        private AudioSource audioSource;

        private void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        public void TakeDamage(float damage) {
            health -= damage;

            if (health <= 0) {
                audioSource.PlayOneShot(deathSound);
                Destroy(this);
            }

            audioSource.PlayOneShot(hurtSound);
        }
    }
}