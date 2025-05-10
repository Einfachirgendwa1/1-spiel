using UnityEngine;

public class Target : MonoBehaviour, IDamageable {
    EnemyHealth enemyHealth;
    AudioSource enemyAudio;
    public AudioClip hurtSound;

    private void Start() {
        enemyAudio = transform.root.GetComponent<AudioSource>();
        enemyHealth = transform.root.GetComponent<EnemyHealth>();
    }

    public void TakeDamage(RaycastHit hit, float damage) {
        if (hit.transform.CompareTag("Enemy"))
        {
            enemyHealth.GetDamage(damage);
            Debug.Log(hit.transform.name + "[from Target Enemy]");
            Debug.Log("Tag is " + hit.transform.tag);
        }
        else if (hit.transform.CompareTag("Head"))
        {
            enemyHealth.GetDamage(100);
            Debug.Log("Headshot on Enemy");
        }

        enemyAudio.PlayOneShot(hurtSound);
    }
}