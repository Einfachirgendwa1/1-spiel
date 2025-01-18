using UnityEngine;

public class EnemyShootBehavior : MonoBehaviour
{
    bool leftClick = false;
    bool reloadButton = false;
    
    float timer = 0;
    GunScriptV2 gun;
    EnemyPlayerDetection playerDetection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gun = this.gameObject.GetComponentInChildren<GunScriptV2>();
        playerDetection = GetComponent<EnemyPlayerDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetection.canSeePlayer)
        {
            if (gun.ammunitionInGun != 0)
            {

            }
            else
            {
                //nachladen
            }
        }
    }
}
