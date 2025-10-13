using Gun;
using Settings;
using UnityEngine;
using Cursor = UI.Cursor;

namespace Player {
    public class PlayerGunController : GunController {
        private readonly Cursor cursor = new();

        public void Update() {
            for (int gun = 1; gun <= 9; gun++) {
                if (gun.Is(Input.GetKeyDown)) {
                    SelectGun(gun - 1);
                }
            }

            if (Action.Shoot.Is(Input.GetKeyDown)) {
                CurrentGun.WantsToShoot(true);
            } else if (Action.Shoot.Is(Input.GetKeyUp) && CurrentGun.automatic) {
                CurrentGun.WantsToShoot(false);
            }

            cursor.str = CurrentGun.DoShoot().ToString();

            CurrentGun.DoReload = Action.Reload.Is(Input.GetKey);
        }
    }
}