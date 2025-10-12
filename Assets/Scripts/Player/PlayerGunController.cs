using Gun;
using Settings;
using UnityEngine;

namespace Player {
    public class PlayerGunController : GunController {
        public void Update() {
            for (int gun = 1; gun <= 9; gun++) {
                if (gun.Is(Input.GetKeyDown)) {
                    SelectGun(gun - 1);
                }
            }

            CurrentGun.WantsToShoot(Action.Shoot.Is(CurrentGun.automatic ? Input.GetKey : Input.GetKeyDown));
            CurrentGun.DoReload = Action.Reload.Is(Input.GetKey);
        }
    }
}