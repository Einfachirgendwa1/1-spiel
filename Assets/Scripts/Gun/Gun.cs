﻿using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gun {
    [Serializable]
    public class Gun : MonoBehaviour {
        private static readonly int FLineColor = Shader.PropertyToID("_Color");
        private static readonly int AmmoId = Animator.StringToHash("Ammo");
        private static readonly int ShootId = Animator.StringToHash("ShouldShoot");
        private static readonly int ReloadId = Animator.StringToHash("ShouldReload");
        private static readonly int UnequipId = Animator.StringToHash("Unequip");
        private static Material firingLineMaterial;

        [Header("Gun Stats")] public bool automatic;
        public int damage;
        public float range;
        public int magazineSize;
        public int firingLineMillis;
        public Animator animator;


        [Range(0f, 1f)] public float weaponSprayX;
        [Range(0f, 1f)] public float weaponSprayY;
        [Range(0f, 15f)] public float recoil;
        private int? ammoBackup;

        [NonSerialized] public GameObject Cam;

        private int shotsInARow;
        private int timeSinceLastShot;

        public int Ammo {
            get => animator.GetInteger(AmmoId);
            set {
                ammoBackup = value;
                animator.SetInteger(AmmoId, value);
            }
        }

        public bool ShouldShoot {
            get => animator.GetBool(ShootId);
            set => animator.SetBool(ShootId, value);
        }

        public bool ShouldReload {
            get => animator.GetBool(ReloadId);
            set => animator.SetBool(ReloadId, value);
        }

        public bool Unequip {
            get => animator.GetBool(UnequipId);
            set => animator.SetBool(UnequipId, value);
        }

        private void Start() {
            if (!firingLineMaterial) {
                firingLineMaterial = new Material(Resources.Load<Material>("Materials/FiringLine"));
            }

            Ammo = magazineSize;
        }

        public IEnumerator Toggle(int trigger) {
            animator.SetTrigger(trigger);
            yield return new WaitForSeconds(.2f);
            animator.ResetTrigger(trigger);
        }

        public void Shoot() {
            Ammo--;

            if (!automatic || Ammo <= 0) {
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

            Vector3 direction = weaponSpray * recoilOffset * Cam.transform.forward;

            Vector3 origin = transform.position;
            bool hitSomething = Physics.Raycast(origin, direction, out RaycastHit hit, range);
            Vector3 end = hitSomething ? hit.point : origin + direction * range;
            StartCoroutine(CreateFiringLine(origin, end));

            if (hitSomething) {
                hit.transform.GetComponent<Target>()?.TakeDamage(damage);
            }
        }

        public void ResetAmmo() {
            Ammo = magazineSize;
        }

        public void OnUnequipEnd() {
            Unequip = false;

            ammoBackup ??= magazineSize;
            Ammo = ammoBackup.Value;
        }

        private IEnumerator CreateFiringLine(Vector3 start, Vector3 end) {
            GameObject go = new("DynamicLine");
            LineRenderer lr = go.AddComponent<LineRenderer>();

            lr.positionCount = 2;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);

            Material firingLine = new(firingLineMaterial);

            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.material = firingLine;
            lr.startColor = Color.red;
            lr.endColor = Color.yellow;

            Stopwatch watch = new();
            watch.Start();


            while (watch.ElapsedMilliseconds < firingLineMillis) {
                float perc = watch.ElapsedMilliseconds / (float)firingLineMillis;
                Color color = firingLine.GetColor(FLineColor);

                // fuck this
                color.b = 1 - perc;

                firingLine.SetColor(FLineColor, color);
                yield return new WaitForSeconds(0.05f);
            }

            Destroy(go);
        }
    }
}