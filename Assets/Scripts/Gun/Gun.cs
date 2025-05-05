using System;
using System.Collections;
using System.Diagnostics;
using Enemies;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Gun {
    [Serializable]
    public class Gun : MonoBehaviour {
        private static readonly int FLineColor = Shader.PropertyToID("_Color");
        private static Material firingLineMaterial;

        [Header("Gun Stats")] public bool automatic;
        public int damage;
        public float range;
        public int reloadTimeMillis;
        public int magazineSize;
        public int shotsPerSecond;
        public int firingLineMillis;

        [Range(0f, 1f)] public float weaponSprayX;
        [Range(0f, 1f)] public float weaponSprayY;
        [Range(0f, 15f)] public float recoil;

        [Header("Scripting")] public GunCallbacks gunCallbacks;

        [NonSerialized] public int Ammo;

        [NonSerialized] public GameObject Cam;
        private bool reloading;

        private Finished runningAnimationFinished;

        private int shotsInARow;
        private int timeSinceLastShot;
        private bool RunningAnimationFinished => runningAnimationFinished.IsFinished;

        private void Start() {
            if (firingLineMaterial == null) {
                firingLineMaterial = new Material(Resources.Load<Material>("Materials/FiringLine"));
            }

            Ammo = magazineSize;
        }

        public void OnEquip() {
            StartCallback(gunCallbacks.StartEquip);
        }

        public void StartCallback(Func<Finished, IEnumerator> func) {
            Assert.IsTrue(RunningAnimationFinished);
            StartCoroutine(func.Invoke(runningAnimationFinished));
        }

        public void Init(GameObject cam) {
            Cam = cam;
        }

        public IEnumerator Shoot() {
            if (!RunningAnimationFinished || Ammo == 0) {
                yield break;
            }

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
            StartCoroutine(CreateFiringLine(origin, hit.transform.position));

            if (hitSomething) {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null) {
                    target.TakeDamage(damage);
                }
            }

            float secondsPerShot = 1f / shotsPerSecond;
            yield return new WaitForSeconds(secondsPerShot);
        }

        public IEnumerator Reload() {
            if (!RunningAnimationFinished) {
                yield break;
            }

            reloading = true;
            StartCallback(gunCallbacks.StartReload);
            yield return new WaitForSeconds(reloadTimeMillis / 1000f);
            Ammo = magazineSize;
            reloading = false;
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