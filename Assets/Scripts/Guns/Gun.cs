using System;
using Guns.Ammunition;
using Sounds;
using Targeting;
using UnityEngine;
using Validation;
using Random = UnityEngine.Random;

namespace Guns {
    [Serializable]
    public class Gun : MonoBehaviour {
        [NonNull] public Animator animator;
        public bool automatic;
        [NonNull] public BulletType bulletType;
        [Positive] public int damage;
        [PositiveNonZero] public float range;
        [PositiveNonZero] public int magazineSize;
        [Range(0f, 1f)] public float weaponSprayX;
        [Range(0f, 1f)] public float weaponSprayY;
        [Range(0f, 15f)] public float recoil;
        [NonNull] public AudioSource audioSource;
        [NonNull] public AudioClip shootSound;
        [NonNull] public AudioClip reloadSound;

        internal int Ammo;
        internal GunController Controller;

        internal int AmmoBackup {
            get => Controller.Ammo[bulletType];
            set => Controller.Ammo[bulletType] = value;
        }

        private void Start() {
            Reload();
        }

        private Vector3 DirectionToCrosshairTarget() {
            Vector3 camPos = Controller.cam.transform.position;

            return Physics.Raycast(camPos, Controller.cam.transform.forward, out RaycastHit crosshairHit)
                ? (crosshairHit.point - transform.position).normalized
                : Controller.cam.transform.forward;
        }

        internal void Reload() {
            int missingBullets = Math.Min(magazineSize - Ammo, AmmoBackup);

            AmmoBackup -= missingBullets;
            Ammo += missingBullets;
        }

        internal void Shoot(int shotsInARow) {
            Ammo--;

            audioSource.PlayOneShot(shootSound);
            GunSound.Create(transform.position, 100);

            float x = Random.Range(-weaponSprayX, weaponSprayX);
            float y = Random.Range(-weaponSprayY, weaponSprayY);

            Quaternion weaponSpray = Quaternion.AngleAxis(x, Vector3.up) * Quaternion.AngleAxis(y, Vector3.right);
            Quaternion recoilOffset = Quaternion.AngleAxis(shotsInARow * recoil, Vector3.right);

            Vector3 direction = weaponSpray * recoilOffset * DirectionToCrosshairTarget();

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, range)) {
                hit.collider.transform.GetComponent<HealthSubpart>()?.TakeDamage(damage);
            }
        }
    }
}