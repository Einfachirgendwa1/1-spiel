using System.Collections;
using TMPro;
using UnityEngine;

public class GunScriptV2 : MonoBehaviour {
    //basic gun properties
    public int magazinSize = 15;
    public int ammunition = 15;

    public float firerate = 1.0f;
    public float reloadTime = 2.0f;
    public float range = 100.0f;
    public float damage = 10.0f;

    public bool isAutomatic;

    //funktional values
    public float timeSinceLastShot; 

    bool isReloading;
    bool fireButtonUp = true;




    AudioSource gunAudio;
    //Animator pistolAnimator;

    public AudioClip shootSound;
    public AudioClip reloadSound;

    public ParticleSystem muzzleFlash;

    public TextMeshProUGUI ammunitionText;

    public Camera cam; //used to have an origin point for the raycast

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        gunAudio = GetComponent<AudioSource>();
        //pistolAnimator = GetComponent<Animator>();

        ammunitionText.SetText("Ammo: " + ammunition);
        timeSinceLastShot = 1.0f / (firerate / 60);
    }

    // Update is called once per frame
    void Update() {
        timeSinceLastShot += Time.deltaTime;
        //ammunitionText.SetText("Ammo: " + ammunition);

        //shooting
        if (Input.GetKey(KeyCode.Mouse0) && CanShoot()) {
            fireButtonUp = false;
            Shoot(); 
        }

        //important for semi automatic:
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            fireButtonUp = true;
        }

            //reloading
            if (Input.GetKeyDown(KeyCode.R) && !isReloading) {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    void Shoot() {
        ammunition--;
        ammunitionText.SetText("Ammo: " + ammunition);

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

    bool CanShoot()
    {
        if (isAutomatic)
        {
            if (ammunition > 0 && !isReloading && timeSinceLastShot >= 1.0f / (firerate / 60))
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
            if (ammunition > 0 && !isReloading && timeSinceLastShot >= 1.0f / (firerate / 60) && fireButtonUp)
            {
                return true;
            }
            else 
            {
                return false;
            }
            
        }
    }
    IEnumerator Reload() {
        gunAudio.PlayOneShot(reloadSound, 1.0f);
        yield return new WaitForSeconds(reloadTime);
        ammunition = magazinSize;
        ammunitionText.SetText("Ammo: " + ammunition);
        isReloading = false;
    }

    /*public void UpdateAmmunitonText()
    {
        ammunitionText.SetText("Ammo: " + ammunition);
    }
    */

}