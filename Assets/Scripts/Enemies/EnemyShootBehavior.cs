using UnityEngine;

public class EnemyShootBehavior : MonoBehaviour
{
    bool fireButton = false;
    bool reloadButton = false;
    public bool enemyIsReloading;


    float timer = 0;
    float timeBetweenShotsMin = 0.5f;
    float timeBetweenShotsMax = 2;
    public GunScriptV2 gun;
    EnemyPlayerDetection playerDetection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gun = this.transform.GetChild(transform.childCount -1).GetComponentInChildren<GunScriptV2>(); //scheint nicht zu funktionieren
        playerDetection = GetComponent<EnemyPlayerDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        gun.timeSinceLastShot += Time.deltaTime;

        if (playerDetection.canSeePlayer)
        {
            if (CanShoot())
            {
                if (gun.isAutomatic)
                {
                       gun.Shoot();
                }
                else if (!gun.isAutomatic)
                {
                    fireButton = true;
                    gun.Shoot();
                    timer = 0;
                    while (timer < 1.0f / (gun.firerate / 60) + 0.5f)
                    {
                        timer += Time.deltaTime;
                    }
                    fireButton = false;
                }
            }
        }
        if (gun.ammunitionInGun == 0 && !enemyIsReloading)
        {
            enemyIsReloading = true;
            StartCoroutine(gun.Reload());
        }
    }

    bool CanShoot()
    {
        if (gun.isAutomatic)
        {
            if (gun.ammunitionInGun > 0 && !enemyIsReloading && gun.timeSinceLastShot >= 1.0f / (gun.firerate / 60))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (gun.ammunitionInGun > 0 && !enemyIsReloading && gun.timeSinceLastShot >= 1.0f / (gun.firerate / 60) && !fireButton)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
