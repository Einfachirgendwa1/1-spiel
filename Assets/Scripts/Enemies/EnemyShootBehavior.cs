using Enemies;
using UnityEngine;

public class EnemyShootBehavior : MonoBehaviour {
    public bool enemyIsReloading;
    public GunScriptV2 gun;
    private bool fireButton;
    private EnemyPlayerDetection playerDetection;

    private float timer;

    private void Start() {
        gun = transform.Find("Weapon Holder").GetComponentInChildren<GunScriptV2>();
        playerDetection = GetComponent<EnemyPlayerDetection>();
    }

    private void Update() {
        if (gun == null) {
            Debug.LogError("Gun is null!");
            return;
        }

        gun.timeSinceLastShot += Time.deltaTime;

        if (playerDetection.canSeePlayer && CanShoot()) {
            gun.Shoot();
        }


        if (gun.ammunitionInGun == 0 && !enemyIsReloading) {
            enemyIsReloading = true;
            StartCoroutine(gun.Reload());
        }
    }

    private bool CanShoot() {
        return gun.ammunitionInGun > 0
               && !enemyIsReloading
               && gun.timeSinceLastShot >= 1.0f / (gun.firerate / 60)
               && (!gun.isAutomatic || !fireButton);
    }
}