using System.Threading.Tasks;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public float enemyHealth = 100;
    [SerializeField] float timeToWait = 1000;

    AudioSource enemyAudio;
    public AudioClip deathSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        enemyAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void GetDamage(float damage) {
        enemyHealth -= damage;
        if (enemyHealth <= 0) {
            Die();
        }
    }

    public async void Die() {
        enemyAudio.PlayOneShot(deathSound);
        //Death animation spielen
        //Task.Delay sorgt für warten d.h. enemy bleibt liegen
        await Task.Delay(3000);
        GameObject.Destroy(gameObject);
    }

}
