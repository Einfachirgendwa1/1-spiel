using System.Collections;
using TMPro;
using UnityEngine;

public class GunScriptV2 : MonoBehaviour
{


    //basic gun properties
    public int magazinSize = 15;
    public int ammunitionInGun;

    public float firerate = 1.0f;
    public float reloadTime = 2.0f;
    public float range = 100.0f;
    public float damage = 10.0f;

    public bool isAutomatic;

    //funktional values
    public float timeSinceLastShot;

    //public bool isReloading;
    PlayerShoot playerShoot;
    EnemyShootBehavior enemyShoot;





    AudioSource gunAudio;
    //Animator pistolAnimator;

    public AudioClip shootSound;
    public AudioClip reloadSound;

    public ParticleSystem muzzleFlash;

    public TextMeshProUGUI ammunitionText;

    public Camera cam; //used to have an origin point for the raycast

    PlayerInventory playerInventory;
    GameObject gunUser;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunUser = this.transform.root.gameObject;
        gunAudio = GetComponent<AudioSource>();
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();

        ammunitionInGun = magazinSize;
        ammunitionText.SetText("Ammo: " + ammunitionInGun);

        playerShoot = gunUser.GetComponent<PlayerShoot>();
        enemyShoot = gunUser.GetComponent<EnemyShootBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
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



    public void Shoot()
    {
        ammunitionInGun--;
        ammunitionText.SetText("Ammo: " + ammunitionInGun);

        //fx
        muzzleFlash.Play();
        gunAudio.PlayOneShot(shootSound, 1.0f);

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
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
    public IEnumerator Reload()
    {
        gunAudio.PlayOneShot(reloadSound, 1.0f);
        yield return new WaitForSeconds(reloadTime);


        if (magazinSize - ammunitionInGun <= playerInventory.amunition)
        {
            playerInventory.amunition -= (magazinSize - ammunitionInGun);
            ammunitionInGun += magazinSize - ammunitionInGun;
        }
        else
        {
            ammunitionInGun += playerInventory.amunition;
            playerInventory.amunition = 0;
        }


        ammunitionText.SetText("Ammo: " + ammunitionInGun);
        if (gunUser = GameObject.Find("Player"))
        {
            playerShoot.playerIsReloading = false;
        }
        else if (gunUser = GameObject.Find("EnemyV2"))
        {
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
