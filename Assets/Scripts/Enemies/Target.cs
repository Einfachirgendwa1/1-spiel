using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;

    AudioSource enemyAudio;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            enemyAudio.PlayOneShot(deathSound);
            Destroy(gameObject);
        }
        enemyAudio.PlayOneShot(hurtSound);
    }

}