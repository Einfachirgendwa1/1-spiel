using UnityEngine;

public class WeaponSwitching : MonoBehaviour {
    public Transform[] weapons;
    [SerializeField] private KeyCode[] keys;
    private int selectedWeapon;

    private void Start() {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++) {
            weapons[i] = transform.GetChild(i);
        }

        Select(selectedWeapon);
    }

    private void Update() {
        for (int i = 0; i < keys.Length; i++) {
            if (Input.GetKeyDown(keys[i]) && i != selectedWeapon) {
                Select(i);
            }
        }
    }

    private void Select(int newWeaponIndex) {
        weapons[selectedWeapon].gameObject.SetActive(false);
        weapons[newWeaponIndex].gameObject.SetActive(true);
        selectedWeapon = newWeaponIndex;

        UpdateGunInOtherClasses(selectedWeapon);
    }

    private void UpdateGunInOtherClasses(int weaponIndex) {
        GameObject.Find("Player").GetComponent<PlayerShoot>().ammunitionText
            .SetText("Ammo: " + weapons[weaponIndex].gameObject.GetComponent<GunScriptV2>().ammunitionInGun);
        weapons[weaponIndex].gameObject.GetComponent<GunScriptV2>().timeSinceLastShot
            = 1.0f / (weapons[weaponIndex].gameObject.GetComponent<GunScriptV2>().firerate / 60);
        weapons[weaponIndex].root.GetComponent<PlayerShoot>().gun = weapons[weaponIndex].GetComponent<GunScriptV2>();
    }
}