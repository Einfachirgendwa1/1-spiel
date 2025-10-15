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

        internal int currentGunIdx;
        internal int nextGunIdx;

        internal Gun CurrentGun => guns[currentGunIdx];

        public void Start() {
            guns = guns.Select(gun => {
                Gun instance = Instantiate(gun, weaponHolder.transform);
                instance.cam = cam;
                instance.controller = this;
                return instance;
            }).ToList();

            RefreshGuns();
        }

        internal void SelectGun(int index) {
            if (index < guns.Count) {
                CurrentGun.DoUnequip = true;
                nextGunIdx = index;
            }
        }

        internal void OnUnequip() {
            currentGunIdx = nextGunIdx;
            RefreshGuns();
            CurrentGun.animator.Play("Equip");
        }

        internal void RefreshGuns() {
            for (int i = 0; i < guns.Count; i++) {
                guns[i].gameObject.SetActive(i == currentGunIdx);
            }
        }
    }
}