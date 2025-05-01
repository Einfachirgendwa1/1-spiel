using TMPro;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    public GunScriptV2 gun; //in weapun switching assignet in l. 21
    public TextMeshProUGUI ammunitionText;

    //funktional values
    public float shotsInARow;
    public bool playerIsReloading;
    public Camera cam;

    private bool fireButtonUp = true;

    //public int amunition = 100;
    private WeaponSwitching gunSwitching;
    private PlayerInventory playerInventory;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        gun.timeSinceLastShot = 1.0f / (gun.firerate / 60);
        ammunitionText.SetText("Ammo: " + gun.ammunitionInGun);
        //gunSwitching = 
        //gun = gunSwitching.weapons[gunSwitching.selectedWeapon].gameObject.GetComponent<GunScriptV2>();
    }

    // Update is called once per frame
    private void Update() {
        gun.timeSinceLastShot += Time.deltaTime;
        //ammunitionText.SetText("Ammo: " + ammunition);

        //shooting
        if (Input.GetKey(KeyCode.Mouse0) && CanShoot()) {
            fireButtonUp = false;
            gun.Shoot();
            shotsInARow += 1;
            ammunitionText.SetText("Ammo: " + gun.ammunitionInGun);
        }

        //important for semi automatic:
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            fireButtonUp = true;
        }

        //reloading
        if (Input.GetKeyDown(KeyCode.R) && !playerIsReloading) {
            if (playerInventory.amunition > 0 && gun.ammunitionInGun != gun.magazinSize) {
                playerIsReloading = true;
                StartCoroutine(gun.Reload());
            }
            // sound, nachricht das player keinen ammo hat
        }

        //decreases the ShotsInARow variable to make the spray work on semi automatic guns
        if (shotsInARow >= 0.3 && fireButtonUp) {
            shotsInARow -= Time.deltaTime * shotsInARow * 0.90f;
        } else if (shotsInARow < 0.3f) {
            shotsInARow = 0;
        }
    }

    private bool CanShoot() {
        if (gun.isAutomatic) {
            if (gun.ammunitionInGun > 0 && !playerIsReloading && gun.timeSinceLastShot >= 1.0f / (gun.firerate / 60)) {
                return true;
            }

            return false;
        }

        if (gun.ammunitionInGun > 0 && !playerIsReloading && gun.timeSinceLastShot >= 1.0f / (gun.firerate / 60) &&
            fireButtonUp) {
            return true;
        }

        return false;
    }
}