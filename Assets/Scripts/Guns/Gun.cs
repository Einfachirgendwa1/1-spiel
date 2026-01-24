using System;
using Guns.Ammunition;
using Targeting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Guns {
    [Serializable]
    public class Gun : MonoBehaviour {
        [Header("Required")] public Animator animator;
        [Header("Gun Settings")] public bool automatic;
        public BulletType bulletType;
        public int damage;
        public float range;
        public int magazineSize;
        [Range(0f, 1f)] public float weaponSprayX;
        [Range(0f, 1f)] public float weaponSprayY;
        [Range(0f, 15f)] public float recoil;
        internal int Ammo;
        internal Func<Vector3> BulletOrigin;
        internal GunController Controller;
        internal Func<Vector3> GetShootDirection;

        internal int AmmoBackup {
            get => Controller.Ammo[bulletType];
            set => Controller.Ammo[bulletType] = value;
        }

        private void Start() {
            Reload();
        }

        internal void Reload() {
            int missingBullets = Math.Min(magazineSize - Ammo, AmmoBackup);

            AmmoBackup -= missingBullets;
            Ammo += missingBullets;
        }

        internal void Shoot(int shotsInARow) {
            Ammo--;

            float x = Random.Range(-weaponSprayX, weaponSprayX);
            float y = Random.Range(-weaponSprayY, weaponSprayY);

            Quaternion weaponSpray = Quaternion.AngleAxis(x, Vector3.up) * Quaternion.AngleAxis(y, Vector3.right);
            Quaternion recoilOffset = Quaternion.AngleAxis(shotsInARow * recoil, Vector3.right);

            Vector3 direction = weaponSpray * recoilOffset * GetShootDirection();

            if (Physics.Raycast(BulletOrigin(), direction, out RaycastHit hit, range)) {
                hit.transform.GetComponent<ITarget>()?.TakeDamage(damage);
            }

            Debug.DrawLine(BulletOrigin(), BulletOrigin() + direction * range, Color.red, 3.0f);
        }
    }
}