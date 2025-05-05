using UnityEngine;

public class Target : MonoBehaviour, IDamageable {
    [SerializeField] private float health = 100f;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    private AudioSource enemyAudio;

    private void Start() {
        enemyAudio = GetComponent<AudioSource>();
    }

    public void TakeDamage(RaycastHit hit, float damage) {
        if (hit.transform.CompareTag("Enemy"))
        {
            health -= damage;
        }
        else if (hit.transform.CompareTag("Head"))
        {
            health -= 100;
            Debug.Log("Headshot on Enemy");
        }

        
        if (health <= 0) {
            enemyAudio.PlayOneShot(deathSound);
            Destroy(gameObject);
        }

        enemyAudio.PlayOneShot(hurtSound);
    }
}