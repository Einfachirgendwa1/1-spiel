using UnityEngine;
using TMPro;
public class WeaponSwitching : MonoBehaviour {
    public Transform[] weapons;
    [SerializeField] private KeyCode[] keys;
    int selectedWeapon = 0;

    private void Select(int newWeaponIndex) {
        weapons[selectedWeapon].gameObject.SetActive(false);
        weapons[newWeaponIndex].gameObject.SetActive(true);
        selectedWeapon = newWeaponIndex;

        UpdateGunInOtherClasses(selectedWeapon);
        
    }

    void UpdateGunInOtherClasses(int weaponIndex)
    {
        GameObject.Find("Player").GetComponent<PlayerShoot>().ammunitionText.SetText("Ammo: " + weapons[weaponIndex].gameObject.GetComponent<GunScriptV2>().ammunitionInGun);
        weapons[weaponIndex].gameObject.GetComponent<GunScriptV2>().timeSinceLastShot = 1.0f / (weapons[weaponIndex].gameObject.GetComponent<GunScriptV2>().firerate / 60);
        weapons[weaponIndex].root.GetComponent<PlayerShoot>().gun = weapons[weaponIndex].GetComponent<GunScriptV2>();
        Debug.Log("gun is assinget");
    }

    void Start() {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++) {
            weapons[i] = transform.GetChild(i);
        }

        Select(selectedWeapon);
    }

    void Update() {
        for (int i = 0; i < keys.Length; i++) {
            if (Input.GetKeyDown(keys[i]) && i != selectedWeapon) {
                Select(i);
            }
        }
    }
}