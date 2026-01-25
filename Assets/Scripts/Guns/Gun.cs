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
        internal GunController Controller;
        private LineRenderer lineRenderer;

        internal int AmmoBackup {
            get => Controller.Ammo[bulletType];
            set => Controller.Ammo[bulletType] = value;
        }

        private void Start() {
            Reload();
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.03f;
        }

        private void Update() {
            Vector3 toTarget = DirectionToCrosshairTarget();

            lineRenderer.SetPositions(new[] {
                transform.position,
                Physics.Raycast(transform.position, toTarget, out RaycastHit hit, range)
                    ? hit.point
                    : transform.position + toTarget * range
            });
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

            float x = Random.Range(-weaponSprayX, weaponSprayX);
            float y = Random.Range(-weaponSprayY, weaponSprayY);

            Quaternion weaponSpray = Quaternion.AngleAxis(x, Vector3.up) * Quaternion.AngleAxis(y, Vector3.right);
            Quaternion recoilOffset = Quaternion.AngleAxis(shotsInARow * recoil, Vector3.right);

            Vector3 camPos = Controller.cam.transform.position;
            Vector3 toTarget = Physics.Raycast(camPos, Controller.cam.transform.forward, out RaycastHit crosshairHit)
                ? (crosshairHit.point - transform.position).normalized
                : Controller.cam.transform.forward;

            Vector3 direction = weaponSpray * recoilOffset * toTarget;

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, range)) {
                hit.transform.GetComponent<ITarget>()?.TakeDamage(damage);
            }
        }
    }
}