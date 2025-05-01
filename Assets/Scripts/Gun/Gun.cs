using System;
using System.Collections;
using Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gun {
    [Serializable]
    public class Gun : MonoBehaviour {
        public bool automatic;
        public int damage;
        public float range;
        public int reloadTimeMillis;
        public int magazineSize;
        public int shotsPerSecond;

        [Range(0f, 1f)] public float weaponSprayX;
        [Range(0f, 1f)] public float weaponSprayY;
        [Range(0f, 15f)] public float recoil;

        private int ammo;

        [NonSerialized] public GameObject Cam;

        private bool reloading;
        private bool shooting;
        private int shotsInARow;
        private int timeSinceLastShot;

        private void Start() {
            ammo = magazineSize;
        }

        public void Init(GameObject cam) {
            Cam = cam;
        }

        public IEnumerator Shoot() {
            if (shooting || reloading || ammo == 0) {
                yield break;
            }

            shooting = true;
            ammo--;

            if (timeSinceLastShot > 1) {
                shotsInARow = 0;
            }

            shotsInARow++;
            timeSinceLastShot = 0;

            float x = Random.Range(-weaponSprayX, weaponSprayX);
            float y = Random.Range(-weaponSprayY, weaponSprayY);

            Quaternion weaponSpray = Quaternion.AngleAxis(x, Vector3.up) * Quaternion.AngleAxis(y, Vector3.right);
            Quaternion recoilOffset = Quaternion.AngleAxis(shotsInARow * recoil, Vector3.right);

            Vector3 direction = weaponSpray * recoilOffset * Cam.transform.forward;

            bool hitSomething = Physics.Raycast(transform.position, direction, out RaycastHit hit, range);

            if (hitSomething) {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null) {
                    target.TakeDamage(damage);
                }
            }

            float secondsPerShot = 1f / shotsPerSecond;
            yield return new WaitForSeconds(secondsPerShot);
            shooting = false;
        }

        public IEnumerator Reload() {
            if (reloading || shooting) {
                yield break;
            }

            reloading = true;
            yield return new WaitForSeconds(reloadTimeMillis / 1000f);
            ammo = magazineSize;
            reloading = false;
        }
    }
}