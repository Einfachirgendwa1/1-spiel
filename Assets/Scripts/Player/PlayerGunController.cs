using Guns;
using Settings.Global.Input;
using UnityEngine;

namespace Player {
    public class PlayerGunController : GunController {
        public new void Update() {
            base.Update();
            for (int gun = 1; gun <= 9; gun++) {
                if (gun.Is(Input.GetKeyDown)) {
                    SelectGun(gun - 1);
                }
            }

            ReadInput(Action.Shoot.Is(CurrentGun.automatic ? Input.GetKey : Input.GetKeyDown), State.Shoot);
            ReadInput(Action.Reload.Is(Input.GetKeyDown), State.Reload);
        }
    }
}