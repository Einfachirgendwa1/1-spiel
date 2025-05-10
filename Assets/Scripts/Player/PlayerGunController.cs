using Gun;
using UnityEngine;

namespace Player {
    public class PlayerGunController : GunController {
        public void Update() {
            for (KeyCode key = KeyCode.Alpha1; key <= KeyCode.Alpha9; key++) {
                if (Input.GetKeyDown(key)) {
                    SelectGun(key - KeyCode.Alpha1);
                }
            }

            if (Input.GetMouseButtonDown(0)) {
                CurrentGun.ShouldShoot = true;
            } else if (Input.GetMouseButtonUp(0)) {
                CurrentGun.ShouldShoot = false;
            }

            CurrentGun.ShouldReload = Input.GetKey(KeyCode.R);
        }
    }
}