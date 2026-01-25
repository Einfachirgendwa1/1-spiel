using Enemies;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Validation;

namespace Targeting {
    public class Health : MonoBehaviour, ITarget {
        [PositiveNonZero] public float health;
        [NonNull] public AudioClip deathSound;
        [NonNull] public AudioSource audioSource;

        public void TakeDamage(float damage) {
            OnDamageTaken?.Invoke(damage);

            health -= damage;

            if (health <= 0) {
                gameObject.GetComponent<EnemyGunController>().enabled = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                audioSource.PlayOneShot(deathSound);
                StartCoroutine(Kill());
            }
        }

        internal event Action<float> OnDamageTaken;

        //temporäre lösung
        private IEnumerator Kill()
        {
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }
    }

    
}