using System.Collections;
using TMPro;
using UnityEngine;

public class GunScriptV2 : MonoBehaviour
{
    //basic gun properties
    public int magazinSize = 15;
    public int ammunition = 15;
    public float firerate = 1.0f;
    public float reloadTime = 2.0f;

    AudioSource gunAudio;
    //Animator pistolAnimator;

    public AudioClip shootSound;
    public AudioClip reloadSound;

    public ParticleSystem muzzleFlash;

    public TextMeshProUGUI ammunitionText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
        //pistolAnimator = GetComponent<Animator>();

        ammunitionText.SetText("Ammo: " + ammunition);
    }

    // Update is called once per frame
    void Update()
    {
        //shooting
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammunition > 0)
        {
            Shoot();
            ammunition -= 1;
            ammunitionText.SetText("Ammo: " + ammunition);
            Debug.Log("ShootV2");
        }

        //reloading
        if (Input.GetKeyDown(KeyCode.R))
        {
           StartCoroutine(Reload()); 
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gunAudio.PlayOneShot(shootSound, 1.0f);
    }

    IEnumerator Reload()
    {
        gunAudio.PlayOneShot(reloadSound, 1.0f);
        yield return new WaitForSeconds(reloadTime);
        ammunition = magazinSize;
        ammunitionText.SetText("Ammo: " + ammunition);
        Debug.Log("ReloadV2");
    }
}
