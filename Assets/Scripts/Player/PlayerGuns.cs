using Gun;
using UnityEngine;

namespace Player {
    public class PlayerGuns : Guns {
        public Vector3 cameraHolderHip;
        public Vector3 cameraHolderAim;
        public float aimSwitchTimeSecs;

        private float interpolation;

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
                    SelectGun(key - KeyCode.Alpha1);
                }
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