using System;
using System.Collections.Generic;
using System.Linq;
using Guns.Ammunition;
using UnityEngine;

namespace Guns {
    [Serializable]
    public abstract class GunController : MonoBehaviour {
        private static readonly int ReloadHash = Animator.StringToHash("Reload");
        private static readonly int ShootHash = Animator.StringToHash("Shoot");
        private static readonly int UnequipHash = Animator.StringToHash("Unequip");
        internal static Array AllStates = Enum.GetValues(typeof(State));

        public GameObject cam;
        public GameObject weaponHolder;
        public List<Gun> guns;

        internal Dictionary<BulletType, int> ammo = BulletTypeExtensions.GetAmmoInit();
        private int currentGunIdx;
        internal Dictionary<State, float> inputBuffer = new();
        private int nextGunIdx;
        internal State state;

        internal Gun CurrentGun => guns[currentGunIdx];

        public void Start() {
            guns = guns //hier wird die gun beim weaponholder instantiated
                .Select(gun => {
                        Gun instance = Instantiate(gun, weaponHolder.transform);
                        instance.cam = cam;
                        instance.controller = this;
                        return instance;
                    }
                )
                .ToList();

            foreach (State key in AllStates) {
                inputBuffer.Add(key, 0);
            }

            RefreshGuns();
        }

        public void Update() {
            foreach (State key in AllStates) {
                inputBuffer[key] = Math.Max(inputBuffer[key] - Time.deltaTime, 0);
            }

            bool reloadMakesSense = CurrentGun.Ammo != CurrentGun.magazineSize && CurrentGun.AmmoBackup != 0;

            CurrentGun.animator.SetBool(UnequipHash, WantsTransition(State.Unequip));
            CurrentGun.animator.SetBool(ReloadHash, WantsTransition(State.Reload) && reloadMakesSense);
            CurrentGun.animator.SetBool(ShootHash, WantsTransition(State.Shoot) && CurrentGun.Ammo > 0);
        }

        internal void SelectGun(int index) {
            if (index < guns.Count && index != currentGunIdx) {
                inputBuffer[State.Unequip] = float.PositiveInfinity;
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
            return inputBuffer[newState] > 0;
        }

        internal bool ActiveOrRequested(State s) {
            return state == s || WantsTransition(s);
        }

        internal void ReadInput(bool input, State newState, float bufferLength = 5) {
            if (input) {
                inputBuffer[newState] = bufferLength;
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
