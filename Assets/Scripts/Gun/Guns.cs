using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gun {
    [Serializable]
    public abstract class Guns : MonoBehaviour {
        private static readonly int Equipped = Animator.StringToHash("Equipped");
        private static readonly int ShootId = Animator.StringToHash("StartShoot");
        private static readonly int ReloadId = Animator.StringToHash("StartReload");
        private static readonly int EquipId = Animator.StringToHash("StartEquip");
        private static readonly int UnequipId = Animator.StringToHash("StartUnequip");

        public List<Gun> guns;
        public GameObject cam;
        public GameObject weaponHolder;

        [NonSerialized] public int CurrentGunIdx;
        private bool reloading;

        public Gun CurrentGun => guns[CurrentGunIdx];

        public void Start() {
            InitGuns();
            RefreshGuns();
        }

        private void InitGuns() {
            weaponHolder = GameObject.Find("Camera Holder/Weapon Holder");

            List<Gun> newGuns = new();
            foreach (Gun newGun in guns.Select(gun => Instantiate(gun, weaponHolder.transform))) {
                newGun.Cam = cam;
                newGuns.Add(newGun);
            }

            guns = newGuns;
        }

        public void SelectGun(int index) {
            if (index < guns.Count) {
                StartCoroutine(DoSelect(index));
            }
        }

        public IEnumerator DoSelect(int index) {
            StartCoroutine(CurrentGun.Toggle(UnequipId));
            yield return new WaitWhile(() => CurrentGun.animator.GetBool(Equipped));

            CurrentGunIdx = index;
            RefreshGuns();
            StartCoroutine(CurrentGun.Toggle(EquipId));
            yield return null;
        }

        public void Shoot() {
            StartCoroutine(CurrentGun.Toggle(ShootId));
        }

        public void Reload() {
            StartCoroutine(CurrentGun.Toggle(ReloadId));
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