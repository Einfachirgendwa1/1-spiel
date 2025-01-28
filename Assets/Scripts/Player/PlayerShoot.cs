using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    WeaponSwitching gunSwitching;
    public GunScriptV2 gun;

    //funktional values
    /*public float timeSinceLastShot;

    bool isReloading;
    bool fireButtonUp = true;
    */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gunSwitching = 
        //gun = gunSwitching.weapons[gunSwitching.selectedWeapon].gameObject.GetComponent<GunScriptV2>();
    }

    // Update is called once per frame
    void Update()
    {
        // einige funktionen in gunscriptV2 müssen public gemacht werden!!


        /*.Log(gun.name);
        gun.timeSinceLastShot += Time.deltaTime;
        //ammunitionText.SetText("Ammo: " + ammunition);

        //shooting
        if (Input.GetKey(KeyCode.Mouse0) && gun.CanShoot())
        {
            fireButtonUp = false;
            gun.Shoot();
        }

        //important for semi automatic:
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            fireButtonUp = true;
        }

        //reloading
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            if (playerInventory.amunition > 0 && ammunitionInGun != magazinSize)
            {
                isReloading = true;
                StartCoroutine(Reload());
            }
            else
            {
                // sound, nachricht das player keinen ammo hat
            }
        */
        }
}
