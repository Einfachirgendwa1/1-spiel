using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gun {
    [Serializable]
    public abstract class GunController : MonoBehaviour {
        public GameObject cam;
        public GameObject weaponHolder;
        public List<Gun> guns;

        private int currentGunIdx;

        internal Gun CurrentGun => guns[currentGunIdx];

        public void Start() {
            guns = guns.Select(gun => {
                Gun instance = Instantiate(gun, weaponHolder.transform);
                instance.cam = cam;
                return instance;
            }).ToList();

            RefreshGuns();
        }

        internal void SelectGun(int index) {
            if (index < guns.Count) {
                DoSelect(index);
            }
        }

        internal void DoSelect(int index) {
            CurrentGun.ShouldUnequip = true;
            CurrentGun.whenUnequipped = () => {
                CurrentGun.ShouldUnequip = false;
                CurrentGun.ShouldReload = false;
                CurrentGun.ShouldShoot = false;

                currentGunIdx = index;
                RefreshGuns();
                CurrentGun.animator.Play("Equip");
            };
        }

        private void RefreshGuns() {
            for (int i = 0; i < guns.Count; i++) {
                guns[i].gameObject.SetActive(i == currentGunIdx);
            }
        }
    }
}