using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gun {
    [Serializable]
    public abstract class Guns : MonoBehaviour {
        public List<Gun> guns;
        public GameObject cam;
        public GameObject weaponHolder;

        [NonSerialized] public int CurrentGunIdx;
        private bool reloading;
        [NonSerialized] public bool SwitchingWeapon;

        public Gun CurrentGun => guns[CurrentGunIdx];

        public void Start() {
            // Wir haben nur prefabs, keine wirklichen GameObjects, daher müssen wir die zuerst instanziieren
            InstantiateGuns();

            foreach (Gun gun in guns) {
                gun.Init(cam);
            }

            RefreshGuns();
        }

        private void InstantiateGuns() {
            weaponHolder = GameObject.Find("Camera Holder/Weapon Holder");

            guns = guns.Select(gun => Instantiate(gun, weaponHolder.transform)).ToList();
        }

        public void SelectGun(int index) {
            if (index < guns.Count) {
                StartCoroutine(DoSelect(index));
            }
        }

        public IEnumerator DoSelect(int index) {
            CurrentGun.OnUnequip();
            yield return new WaitWhile(() => CurrentGun.Busy);
            CurrentGunIdx = index;
            RefreshGuns();
            CurrentGun.OnEquip();
        }

        public void Shoot() {
            if (!SwitchingWeapon) {
                StartCoroutine(CurrentGun.Shoot());
            }
        }

        public void Reload() {
            if (!SwitchingWeapon) {
                StartCoroutine(CurrentGun.Reload());
            }
        }

        private void DeactivateInactiveGuns() {
            for (int i = 0; i < guns.Count; i++) {
                if (i != CurrentGunIdx) {
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