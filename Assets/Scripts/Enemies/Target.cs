using UnityEngine;

public class Target : MonoBehaviour, IDamageable {
    private float health = 100f;
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;
    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            audioSource2.Play();
            Destroy(gameObject);
        }
        audioSource1.Play();
    }

}