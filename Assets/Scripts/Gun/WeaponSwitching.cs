using UnityEngine;
using TMPro;
public class WeaponSwitching : MonoBehaviour {
    [SerializeField] private Transform[] weapons;
    [SerializeField] private KeyCode[] keys;
    private int selectedWeapon = 0;

    private void Select(int newWeaponIndex) {
        weapons[selectedWeapon].gameObject.SetActive(false);
        weapons[newWeaponIndex].gameObject.SetActive(true);
        selectedWeapon = newWeaponIndex;
        weapons[newWeaponIndex].gameObject.GetComponent<GunScriptV2>().ammunitionText.SetText("Ammo: " + weapons[newWeaponIndex].gameObject.GetComponent<GunScriptV2>().ammunition);
        weapons[newWeaponIndex].gameObject.GetComponent<GunScriptV2>().timeSinceLastShot = 1.0f / (weapons[newWeaponIndex].gameObject.GetComponent<GunScriptV2>().firerate / 60);
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