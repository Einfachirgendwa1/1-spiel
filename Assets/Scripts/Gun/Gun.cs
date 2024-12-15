using System.Collections;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour {
    [SerializeField] GunData gunData;
    [SerializeField] Transform cam;

    public TextMeshProUGUI ammoText;

    AudioSource gunAudio;
    Animator pistolAnimator;

    public AudioClip shootSound;
    public AudioClip reloadSound;

    float timeSinceLastShot;
    // Warum brauchen wir das? Spezifiziert die Gun nicht die fireRate?
    float fireRate = 1.0f;

    public ParticleSystem muzzleFlash;

    void Start() {
        // Das wirkt viel zu kompliziert für das, was wir machen wollen
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

        gunAudio = GetComponent<AudioSource>();
        pistolAnimator = GetComponent<Animator>();
    }

    // Warum ist das wichtig?
    private void OnDisable() => gunData.reloading = false;

    public void StartReload() {
        // Was macht activeSelf?
        if (!gunData.reloading && gameObject.activeSelf) {
            StartCoroutine(Reload());
            pistolAnimator.SetTrigger("Reload");
            gunAudio.PlayOneShot(reloadSound, 1);
        }
    }

    // Müssen wir das über eine Coroutine machen oder können wir einfach WaitForSeconds(gunData.reloadTime) am Ende haben?
    private IEnumerator Reload() {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }

    // Warum fireRate / ...?
    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > fireRate / (gunData.fireRate / 60f);
    private void Shoot() {
        if (gunData.currentAmmo > 0) {
            if (CanShoot()) {
                // Was zum Teufel macht out RaycastHit hitInfo?
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance)) {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                }

                muzzleFlash.Play();
                gunAudio.PlayOneShot(shootSound, 1.0f);
                pistolAnimator.SetTrigger("Shoot");

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
            }
        }
    }


    void Update() {
        // Könnten wir timeSinceLastShot nicht wie Reload über eine Coroutine machen?
        timeSinceLastShot += Time.deltaTime;

        ammoText.SetText("Ammo: " + gunData.currentAmmo);
    }
}
