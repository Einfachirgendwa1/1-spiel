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
    }

    // Update is called once per frame
    void Update() {
        
        //shooting
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammunition > 0) {
            Shoot(); 
        }

        //reloading
        if (Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine(Reload());
        }
    }

    void Shoot() {
        ammunitionText.SetText("Ammo: " + ammunition);
        ammunition--;

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }

        //fx
        muzzleFlash.Play();
        gunAudio.PlayOneShot(shootSound, 1.0f);
    }

    IEnumerator Reload() {
        gunAudio.PlayOneShot(reloadSound, 1.0f);
        yield return new WaitForSeconds(reloadTime);
        ammunition = magazinSize;
        ammunitionText.SetText("Ammo: " + ammunition);
    }
}