using Gun;
using Settings;
using UnityEngine;

namespace Player {
    public class PlayerGunController : GunController {
        public void Update() {
            for (int gun = 1; gun <= 9; gun++) {
                if (gun.Is(Input.GetKeyDown)) {
                    SelectGun(gun);
                }
            }

            if (Action.Shoot.Is(Input.GetKeyDown)) {
                CurrentGun.ShouldShoot = true;
            } else if (Action.Shoot.Is(Input.GetKeyUp)) {
                CurrentGun.ShouldShoot = false;
            }

            CurrentGun.ShouldReload = Action.Reload.Is(Input.GetKey);
        }
    }
}