using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gun {
    [Serializable]
    public abstract class Guns : MonoBehaviour {
        public List<Gun> guns;
        public GameObject cam;

        private int currentGunIdx;
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

        private void InstantiateGuns() {
            GameObject weaponHolder = GameObject.Find("Camera Holder/Weapon Holder");

            guns = guns.Select(gun => Instantiate(gun, weaponHolder.transform)).ToList();
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
    }
}