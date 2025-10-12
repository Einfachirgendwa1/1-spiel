using System;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;
using Cursor = UI.Cursor;

namespace Gun {
    [Serializable]
    public class Gun : MonoBehaviour {
        private static readonly int ReloadHash = Animator.StringToHash("Reload");
        private static readonly int ShootHash = Animator.StringToHash("Shoot");
        private static readonly int UnequipHash = Animator.StringToHash("Unequip");

        private static readonly Cursor reload = new() {
            name = "Should Reload"
        };

        private static readonly Cursor shoot = new() {
            name = "Should Shoot"
        };

        private static readonly Cursor unequip = new() {
            name = "Should Unequip"
        };

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
        private int shotsInARow;
        private int timeSinceLastShot;
        [CanBeNull] internal Action whenUnequipped;

        internal bool ShouldReload {
            set {
                animator.SetBool(ReloadHash, value);
                reload.description = value.ToString();
            }
        }

        internal bool ShouldShoot {
            set {
                animator.SetBool(ShootHash, value);
                shoot.description = value.ToString();
            }
        }

        internal bool ShouldUnequip {
            set {
                animator.SetBool(UnequipHash, value);
                unequip.description = value.ToString();
            }
        }

        private void Start() {
            Ammo = magazineSize;
        }

        public void Shoot() {
            if (!automatic || Ammo-- <= 0) {
                ShouldShoot = false;
            }

            if (timeSinceLastShot > 1) {
                shotsInARow = 0;
            }

            shotsInARow++;
            timeSinceLastShot = 0;

            float x = Random.Range(-weaponSprayX, weaponSprayX);
            float y = Random.Range(-weaponSprayY, weaponSprayY);

            Quaternion weaponSpray = Quaternion.AngleAxis(x, Vector3.up) * Quaternion.AngleAxis(y, Vector3.right);
            Quaternion recoilOffset = Quaternion.AngleAxis(shotsInARow * recoil, Vector3.right);

            Vector3 direction = weaponSpray * recoilOffset * cam.transform.forward;

            Vector3 origin = transform.position;

            if (Physics.Raycast(origin, direction, out RaycastHit hit, range)) {
                hit.transform.GetComponent<Health>()?.TakeDamage(damage);
            }
        }
    }
}