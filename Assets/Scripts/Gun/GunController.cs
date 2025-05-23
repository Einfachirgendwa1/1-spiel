﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gun {
    [Serializable]
    public abstract class GunController : MonoBehaviour {
        public List<Gun> guns;
        public GameObject cam;
        public GameObject weaponHolder;

        [NonSerialized] public int CurrentGunIdx;
        private bool reloading;

        public Gun CurrentGun => guns[CurrentGunIdx];

        public void Start() {
            List<Gun> newGuns = new();
            foreach (Gun newGun in guns.Select(gun => Instantiate(gun, weaponHolder.transform))) {
                newGun.Cam = cam;
                newGuns.Add(newGun);
            }

            guns = newGuns;

            RefreshGuns();
        }

        public void SelectGun(int index) {
            if (index < guns.Count) {
                StartCoroutine(DoSelect(index));
            }
        }

        public IEnumerator DoSelect(int index) {
            CurrentGun.Unequip = true;
            yield return new WaitWhile(() => CurrentGun.Unequip);

            CurrentGunIdx = index;
            RefreshGuns();
            CurrentGun.animator.Play("Equip");
            yield return null;
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