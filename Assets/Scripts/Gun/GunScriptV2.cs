using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunScriptV2 : MonoBehaviour {
    //basic gun properties
    public int magazinSize = 15;
    public int ammunitionInGun;

    public float firerate = 1.0f;
    public float reloadTime = 2.0f;
    public float range = 200.0f;
    public float damage = 10.0f;

    [Range(0f, 1f)] public float weaponSprayX;

    [Range(0f, 1f)] public float weaponSprayY;

    [Range(0f, 15f)] public float recoil;

    public bool isAutomatic;

    //functional values
    public float timeSinceLastShot;
    //Animator pistolAnimator;

    public AudioClip shootSound;
    public AudioClip reloadSound;

    public ParticleSystem muzzleFlash;


    public GameObject cam; //used to have an origin point for the raycast
    private EnemyShootBehavior enemyShoot;


    private AudioSource gunAudio;
    private GameObject gunUser;

    private PlayerInventory playerInventory;


    //public bool isReloading;
    private PlayerShoot playerShoot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        gunUser = transform.root.gameObject;
        gunAudio = GetComponent<AudioSource>();
        playerInventory = gunUser.GetComponent<PlayerInventory>();

        ammunitionInGun = magazinSize;


        playerShoot = gunUser.GetComponent<PlayerShoot>();
        enemyShoot = gunUser.GetComponent<EnemyShootBehavior>();

        cam = gunUser.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    private void Update() {
        /*
        if (player = GameObject.Find("Player"))
        {
            timeSinceLastShot += Time.deltaTime;
            //ammunitionText.SetText("Ammo: " + ammunition);

            //shooting
            if (Input.GetKey(KeyCode.Mouse0) && CanShoot())
            {
                fireButtonUp = false;
                Shoot();
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

            }


        if (gunUser = GameObject.Find("EnemyV2"))
        {
            Debug.Log("its the enemy baby");
        }
        */
    }


    public void Shoot() {
        ammunitionInGun--;

        //fx
        muzzleFlash.Play();
        gunAudio.PlayOneShot(shootSound, 1.0f);

        // Vector3 spray = new Vector3 (UnityEngine.Random.Range(0, weaponSpray * Mathf.PI * 180/Mathf.PI/4), UnityEngine.Random.Range(0, weaponSpray * Mathf.PI * 180 / Mathf.PI / 4), 0).normalized; //der weapon spray wert wird als wert im bogenma� interpretiert.
        // er wird ins gradma� umgerechnet. das ergebniss wird durch 4 geteielt
        // um einen maximalen spray von 45� zu erhalten (90 w�ren unrealistisch)
        RaycastHit hit;
        float x = Random.Range(-weaponSprayX, weaponSprayX);
        float y = Random.Range(-weaponSprayY, weaponSprayY);
        float shotsInaRow = 1;
        try {
            shotsInaRow = playerShoot.shotsInARow;
        }
        catch (NullReferenceException) {
            shotsInaRow = 1;
        }


        if (Physics.Raycast(cam.transform.position,
                Quaternion.AngleAxis(x, Vector3.up) * Quaternion.AngleAxis(y, Vector3.right) *
                Quaternion.AngleAxis(shotsInaRow * recoil, Vector3.right) * cam.transform.forward, out hit,
                range)) //cam.transform.forward
        {
            Debug.DrawRay(cam.transform.position,
                Quaternion.AngleAxis(x, Vector3.up) * Quaternion.AngleAxis(y, Vector3.right) *
                Quaternion.AngleAxis(shotsInaRow * recoil, Vector3.right) * (cam.transform.forward * 20), Color.red,
                4f);

            Target target = hit.transform.GetComponent<Target>();
            HealthManager player = hit.transform.GetComponent<HealthManager>();
            if (target != null) {
                target.TakeDamage(damage);
            }
            else if (player != null) {
                player.GetHurt(damage);
            }
        }


        timeSinceLastShot = 0;
    }

    /*bool CanShoot()
    {
        if (isAutomatic)
        {
            if (ammunitionInGun > 0 && !isReloading && timeSinceLastShot >= 1.0f / (firerate / 60))
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
            if (ammunitionInGun > 0 && !isReloading && timeSinceLastShot >= 1.0f / (firerate / 60) && fireButtonUp)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }

*/


    public IEnumerator Reload() {
        gunAudio.PlayOneShot(reloadSound, 1.0f);
        yield return new WaitForSeconds(reloadTime);


        if (magazinSize - ammunitionInGun <= playerInventory.amunition) {
            playerInventory.amunition -= magazinSize - ammunitionInGun;
            ammunitionInGun += magazinSize - ammunitionInGun;
        }
        else {
            ammunitionInGun += playerInventory.amunition;
            playerInventory.amunition = 0;
        }


        /*if (gunUser = GameObject.Find("Player"))
        {
            playerShoot.playerIsReloading = false;
            playerShoot.ammunitionText.SetText("Ammo: " + playerShoot.gun.ammunitionInGun);
        }
        else if (gunUser = GameObject.Find("EnemyV2"))
        {
            enemyShoot.enemyIsReloading = false;
        }*/
        try {
            playerShoot.playerIsReloading = false;
            playerShoot.ammunitionText.SetText("Ammo: " + playerShoot.gun.ammunitionInGun);
            playerShoot.shotsInARow = 0;
        }
        catch (NullReferenceException) {
            enemyShoot.enemyIsReloading = false;
        }
    }
}

/*
public void UpdateAmmunitonText()
{
    ammunitionText.SetText("Ammo: " + ammunition);
}

*/