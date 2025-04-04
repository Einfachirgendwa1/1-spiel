using UnityEngine;
using System.Collections;
using static UnityEngine.Android.AndroidGame;
using TMPro;
using System.Threading;

public class PlayerShoot : MonoBehaviour
{
    //public int amunition = 100;
    WeaponSwitching gunSwitching;
    public GunScriptV2 gun; //in weapun switching assignet in l. 21
    PlayerInventory playerInventory;
    public TextMeshProUGUI ammunitionText;

    //funktional values
    public float shotsInARow = 0;
    public bool playerIsReloading;
    bool fireButtonUp = true;
    public Camera cam;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        gun.timeSinceLastShot = 1.0f / (gun.firerate / 60);
        ammunitionText.SetText("Ammo: " + gun.ammunitionInGun);
        //gunSwitching = 
        //gun = gunSwitching.weapons[gunSwitching.selectedWeapon].gameObject.GetComponent<GunScriptV2>();
    }

    // Update is called once per frame
    void Update()
    {

        gun.timeSinceLastShot += Time.deltaTime;
        //ammunitionText.SetText("Ammo: " + ammunition);

        //shooting
        if (Input.GetKey(KeyCode.Mouse0) && CanShoot())
        {
            fireButtonUp = false;
            gun.Shoot();
            shotsInARow += 1;
            ammunitionText.SetText("Ammo: " + gun.ammunitionInGun);
        }

        //important for semi automatic:
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            fireButtonUp = true;
            
        }

        //reloading
        if (Input.GetKeyDown(KeyCode.R) && !playerIsReloading)
        {
            if (playerInventory.amunition > 0 && gun.ammunitionInGun != gun.magazinSize)
            {
                playerIsReloading = true;
                StartCoroutine(gun.Reload());
            }
            else
            {
                // sound, nachricht das player keinen ammo hat
            }
        }

        //decreases the ShotsInARow variable to make the spray work on semi automatic guns
        if (shotsInARow >= 0.3 && fireButtonUp)
        {
            shotsInARow -= Time.deltaTime * shotsInARow * 0.90f;
        }
        else if ( shotsInARow < 0.3f)
        {
            shotsInARow = 0;
        }
    }

    bool CanShoot()
    {
        if (gun.isAutomatic)
        {
            if (gun.ammunitionInGun > 0 && !playerIsReloading && gun.timeSinceLastShot >= 1.0f / (gun.firerate / 60))
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
            if (gun.ammunitionInGun > 0 && !playerIsReloading && gun.timeSinceLastShot >= 1.0f / (gun.firerate / 60) && fireButtonUp)
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
