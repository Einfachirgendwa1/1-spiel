using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gun {
    [Serializable]
    public abstract class Guns : MonoBehaviour {
        public List<Gun> guns;
        public GameObject cam;
        public Vector3 gunHolderPos;

        private int currentGunIdx;
        private GameObject gunParent;
        private bool reloading;

        public Gun CurrentGun => guns[currentGunIdx];

        public void Start() {
            // Wir haben nur prefabs, keine wirklichen GameObjects, daher müssen wir die zuerst instanziieren
            InstantiateGuns();

            foreach (Gun gun in guns) {
                gun.Init(cam);
            }

            RefreshGuns();
        }

        public virtual void Update() {
            gunParent.transform.localPosition = gunHolderPos;
        }

        private void InstantiateGuns() {
            gunParent = new GameObject("Guns");
            gunParent.transform.SetParent(transform);

            guns = guns.Select(gun => Instantiate(gun, gunParent.transform)).ToList();
        }

        public void SelectGun(int index) {
            if (index < guns.Count) {
                currentGunIdx = index;
                RefreshGuns();
            }
        }

        public void Shoot() {
            StartCoroutine(CurrentGun.Shoot());
        }

        public void Reload() {
            StartCoroutine(CurrentGun.Reload());
        }

        private void DeactivateInactiveGuns() {
            for (int i = 0; i < guns.Count; i++) {
                if (i != currentGunIdx) {
                    guns[i].gameObject.SetActive(false);
                }
            }
        }

        private void RefreshGuns() {
            DeactivateInactiveGuns();
            CurrentGun.gameObject.SetActive(true);
        }

        public void RotateGun() {
            float mouseX = Input.GetAxisRaw("Mouse X") * 4;
            float mouseY = Input.GetAxisRaw("Mouse Y") * 4;

            Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

            Quaternion targetRotation = rotationX * rotationY;

            CurrentGun.transform.localRotation = Quaternion.Slerp(
                CurrentGun.transform.localRotation,
                targetRotation,
                8 * Time.deltaTime
            );
        }
    }
}