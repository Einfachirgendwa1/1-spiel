using System.Collections;
using UnityEngine;

namespace Enemies {
    public class EnemyHealth : MonoBehaviour {
        public float enemyHealth = 100;
        [SerializeField] private float timeToWait = 1;
        public AudioClip deathSound;

        private AudioSource enemyAudio;

        private void Start() {
            enemyAudio = GetComponent<AudioSource>();
        }

        public void GetDamage(float damage) {
            enemyHealth -= damage;
            if (enemyHealth <= 0) {
                StartCoroutine(Die());
            }
        }

        private IEnumerator Die() {
            enemyAudio.PlayOneShot(deathSound);
            yield return new WaitForSeconds(timeToWait);
            Destroy(gameObject);
        }
    }
}