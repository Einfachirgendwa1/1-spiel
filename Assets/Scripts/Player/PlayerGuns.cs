using Gun;
using UnityEngine;

namespace Player {
    public class PlayerGuns : Guns {
        public void Update() {
            SelectGunOnKeypress();
            ShootOnKeypress();
            ReloadOnKeypress();
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