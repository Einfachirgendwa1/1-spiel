using UnityEngine;

public class Target : MonoBehaviour, IDamageable {
    [SerializeField] private float health = 100f;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    private AudioSource enemyAudio;

    private void Start() {
        enemyAudio = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            enemyAudio.PlayOneShot(deathSound);
            Destroy(gameObject);
        }

        enemyAudio.PlayOneShot(hurtSound);
    }
}