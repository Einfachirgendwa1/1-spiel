using System;
using System.Collections;
using System.Diagnostics;
using Enemies;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Gun {
    [Serializable]
    public class Gun : MonoBehaviour {
        private static readonly int FLineColor = Shader.PropertyToID("_Color");
        private static readonly int AmmoId = Animator.StringToHash("Ammo");
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

        [NonSerialized] public GameObject Cam;

        private int shotsInARow;
        private int timeSinceLastShot;

        public int Ammo {
            get => animator.GetInteger(AmmoId);
            set => animator.SetInteger(AmmoId, value);
        }

        private void Start() {
            if (firingLineMaterial == null) {
                firingLineMaterial = new Material(Resources.Load<Material>("Materials/FiringLine"));
            }

            Ammo = magazineSize;
        }

        public IEnumerator Toggle(int trigger) {
            animator.SetTrigger(trigger);
            yield return new WaitForSeconds(1f);
            animator.ResetTrigger(trigger);
        }

        public void ResetAmmo() {
            Ammo = magazineSize;
        }

        public void Shoot() {
            Ammo--;


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
                Target target = hit.transform.GetComponent<Target>();
                if (target != null) {
                    target.TakeDamage(damage);
                }
            }
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