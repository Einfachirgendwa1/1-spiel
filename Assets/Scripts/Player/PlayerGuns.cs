using System.Collections;
using Gun;
using UnityEngine;

namespace Player {
    public class PlayerGuns : Guns {
        public Vector3 cameraHolderHip;
        public Vector3 cameraHolderAim;
        public float aimSwitchTimeSecs;
        public float weaponSwitchY;
        public float weaponSwitchSpeed;

        private float interpolation;
        private Coroutine switchWeapon;

        public void Update() {
            SelectGunOnKeypress();
            HandleAim();
            ShootOnKeypress();
            ReloadOnKeypress();
        }

        private void HandleAim() {
            interpolation += (Input.GetMouseButton(1) ? Time.deltaTime : -Time.deltaTime) / aimSwitchTimeSecs;
            interpolation = Mathf.Clamp(interpolation, 0, 1);

            weaponHolder.transform.localPosition = Vector3.Lerp(cameraHolderHip, cameraHolderAim, interpolation);
        }

        private void SelectGunOnKeypress() {
            for (KeyCode key = KeyCode.Alpha1; key <= KeyCode.Alpha9; key++) {
                if (Input.GetKeyDown(key)) {
                    if (switchWeapon != null) {
                        StopCoroutine(switchWeapon);
                    }

                    switchWeapon = StartCoroutine(SwitchGun(key - KeyCode.Alpha1));
                }
            }
        }

        private IEnumerator SwitchGun(int index) {
            if (index != CurrentGunIdx) {
                while (weaponHolder.transform.localPosition.y - weaponSwitchY > 0.1f) {
                    Vector3 pos = weaponHolder.transform.localPosition;
                    Vector3 target = pos;
                    target.y = weaponSwitchY;
                    weaponHolder.transform.localPosition = Vector3.MoveTowards(pos, target, weaponSwitchSpeed);
                    yield return new WaitForEndOfFrame();
                }

                SelectGun(index);
            }

            while ((weaponHolder.transform.localPosition - cameraHolderHip).magnitude > 0.1f) {
                Vector3 pos = weaponHolder.transform.localPosition;
                weaponHolder.transform.localPosition = Vector3.MoveTowards(pos, cameraHolderHip, weaponSwitchSpeed);
                yield return new WaitForEndOfFrame();
            }
        }

        private void ShootOnKeypress() {
            if (CurrentGun.automatic ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0)) {
                Shoot();
            }
        }

        private void ReloadOnKeypress() {
            if (Input.GetKey(KeyCode.R)) {
                Reload();
            }
        }
    }
}