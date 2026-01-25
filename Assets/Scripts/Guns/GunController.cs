using System;
using System.Collections.Generic;
using System.Linq;
using Guns.Ammunition;
using NUnit.Framework;
using UnityEngine;
using Validation;

namespace Guns {
    [Serializable]
    public abstract class GunController : MonoBehaviour, IValidate {
        private static readonly int ReloadHash = Animator.StringToHash("Reload");
        private static readonly int ShootHash = Animator.StringToHash("Shoot");
        private static readonly int UnequipHash = Animator.StringToHash("Unequip");
        internal static Array AllStates = Enum.GetValues(typeof(State));

        public GameObject cam;
        public GameObject weaponHolder;
        public List<Gun> guns;

        internal Dictionary<BulletType, int> Ammo = BulletTypeExtensions.GetAmmoInit();
        private int currentGunIdx;
        internal Dictionary<State, float> InputBuffer = new();
        private int nextGunIdx;
        internal State State;

        internal Gun CurrentGun => guns[currentGunIdx];

        public void Start() {
            guns = guns
                   .Select(gun => Instantiate(gun, weaponHolder.transform).Also(ts => ts.Controller = this))
                   .ToList();

            foreach (State key in AllStates) {
                InputBuffer.Add(key, 0);
            }

            RefreshGuns();
        }

        public void Update() {
            foreach (State key in AllStates) {
                InputBuffer[key] = Math.Max(InputBuffer[key] - Time.deltaTime, 0);
            }

            bool reloadMakesSense = CurrentGun.Ammo != CurrentGun.magazineSize && CurrentGun.AmmoBackup != 0;

            CurrentGun.animator.SetBool(UnequipHash, WantsTransition(State.Unequip));
            CurrentGun.animator.SetBool(ReloadHash, WantsTransition(State.Reload) && reloadMakesSense);
            CurrentGun.animator.SetBool(ShootHash, WantsTransition(State.Shoot) && CurrentGun.Ammo > 0);
        }

        public void Validate() {
            Assert.NotNull(cam, "cam != null");
            Assert.NotNull(weaponHolder, "weaponHolder != null");
            Assert.NotZero(guns.Count, "guns.Count != 0");

            guns.ForEach(gun => Assert.NotNull(gun, "gun != null"));
        }

        internal void SelectGun(int index) {
            if (index < guns.Count && index != currentGunIdx) {
                InputBuffer[State.Unequip] = float.PositiveInfinity;
                nextGunIdx = index;
            }
        }

        internal void OnUnequip() {
            CurrentGun.transform.localPosition = Vector3.zero;
            currentGunIdx = nextGunIdx;
            RefreshGuns();
            CurrentGun.animator.Play("Equip");
        }

        internal void RefreshGuns() {
            for (int i = 0; i < guns.Count; i++) {
                guns[i].gameObject.SetActive(i == currentGunIdx);
            }
        }

        internal bool WantsTransition(State newState) {
            return InputBuffer[newState] > 0;
        }

        internal bool ActiveOrRequested(State s) {
            return State == s || WantsTransition(s);
        }

        internal void ReadInput(bool input, State newState, float bufferLength = 5) {
            if (input) {
                InputBuffer[newState] = bufferLength;
            }
        }
    }


    internal enum State {
        Idle,
        Shoot,
        Reload,
        Equip,
        Unequip
    }
}