using UnityEngine;

public class EnemyShootBehavior : MonoBehaviour
{
    bool leftClick = false;
    bool reloadButton = false;
    
    float timer = 0;
    float timeBetweenShotsMin = 0.5f;
    float timeBetweenShotsMax = 2;
    GunScriptV2 gun;
    EnemyPlayerDetection playerDetection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gun = this.transform.GetChild(-1).GetComponentInChildren<GunScriptV2>();
        playerDetection = GetComponent<EnemyPlayerDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetection.canSeePlayer)
        {
            if (gun.ammunitionInGun != 0)
            {
                reloadButton = false;
                if (gun.isAutomatic)
                {
                       leftClick = true;
                }
                else if (!gun.isAutomatic)
                {
                    leftClick = true;
                    timer = 0;
                    while (timer < 0)
                    {
                        timer += Time.deltaTime;
                    }
                    leftClick = false;
                }
            }
            else
            {
                leftClick = false;
                reloadButton = true;
            }
        }
    }
}
