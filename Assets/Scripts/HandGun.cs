using System;
using System.Collections;
using UnityEngine;

public class HandGun : MonoBehaviour
{

    [SerializeField] GunData gunData;
    [SerializeField] Transform muzzle;

    float timeSinceLastShot;
    float fireRate = 1.0f;

    void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
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
                    Debug.Log(hitInfo.transform.name);
                }

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
    }

    private void OnGunShot()
    {

    }
}
