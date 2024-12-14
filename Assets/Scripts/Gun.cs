using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class HandGun : MonoBehaviour
{

    [SerializeField] GunData gunData;
    [SerializeField] Transform muzzle;

    public TextMeshProUGUI ammoText;

    AudioSource gunAudio;
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
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
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
                if (Physics.Raycast(muzzle.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                }

                muzzleFlash.Play();
                gunAudio.PlayOneShot(shootSound, 1.0f);

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzle.position, transform.forward);
        ammoText.SetText("Ammo: " + gunData.currentAmmo);
    }

    private void OnGunShot()
    {

    }
}
