using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{

    [SerializeField] GunData gunData;
    [SerializeField] Transform cam;

    public TextMeshProUGUI ammoText;

    AudioSource gunAudio;
    Animator pistolAnimator;

    public AudioClip shootSound;
    public AudioClip reloadSound;

    float timeSinceLastShot;
    float fireRate = 1.0f;

    public ParticleSystem muzzleFlash;

    void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

        gunAudio = GetComponent<AudioSource>();
        pistolAnimator = GetComponent<Animator>();
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
        {
            StartCoroutine(Reload());
            pistolAnimator.SetTrigger("Reload");
            gunAudio.PlayOneShot(reloadSound, 1);
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }
    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > fireRate / (gunData.fireRate / 60f);
    private void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                }

                muzzleFlash.Play();
                gunAudio.PlayOneShot(shootSound, 1.0f);
                pistolAnimator.SetTrigger("Shoot");

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        ammoText.SetText("Ammo: " + gunData.currentAmmo);
    }

    private void OnGunShot()
    {

    }
}
