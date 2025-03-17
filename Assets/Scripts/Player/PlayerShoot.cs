using UnityEngine;
using System.Collections;
using static UnityEngine.Android.AndroidGame;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    //public int amunition = 100;
    WeaponSwitching gunSwitching;
    public GunScriptV2 gun; //in weapun switching assignet
    PlayerInventory playerInventory;
    public TextMeshProUGUI ammunitionText;

    //funktional values
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
