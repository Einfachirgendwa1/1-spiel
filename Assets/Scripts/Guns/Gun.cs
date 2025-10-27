using System;
using Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Guns {
    [Serializable]
    public class Gun : MonoBehaviour {
        [Header("Required")] public Animator animator;
        [Header("Gun Settings")] public bool automatic;
        public int damage;
        public float range;
        public int magazineSize;
        [Range(0f, 1f)] public float weaponSprayX;
        [Range(0f, 1f)] public float weaponSprayY;
        [Range(0f, 15f)] public float recoil;

        internal int Ammo;
        internal GameObject cam;
        internal GunController controller;

        private void Start() => Ammo = magazineSize;

        public void Shoot(int shotsInARow) {
            Ammo--;

            float x = Random.Range(-weaponSprayX, weaponSprayX);
            float y = Random.Range(-weaponSprayY, weaponSprayY);

            Quaternion weaponSpray = Quaternion.AngleAxis(x, Vector3.up) * Quaternion.AngleAxis(y, Vector3.right);
            Quaternion recoilOffset = Quaternion.AngleAxis(shotsInARow * recoil, Vector3.right);

            Vector3 direction = weaponSpray * recoilOffset * cam.transform.forward;

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, range)) {
                hit.transform.GetComponent<Health>()?.TakeDamage(damage);
            }
        }
    }
}