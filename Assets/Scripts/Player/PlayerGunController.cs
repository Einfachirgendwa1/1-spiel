using Guns;
using Settings.Global.Input;
using UnityEngine;

namespace Player {
    public class PlayerGunController : GunController {
        public float pitch;

        public new void Update() {
            base.Update();

            Debug.DrawRay(cam.transform.position, FacingDirection() * 100, Color.green);

            for (int gun = 1; gun <= 9; gun++) {
                if (gun.Is(Input.GetKeyDown)) {
                    SelectGun(gun - 1);
                }
            }

            ReadInput(Action.Shoot.Is(CurrentGun.automatic ? Input.GetKey : Input.GetKeyDown), State.Shoot);
            ReadInput(Action.Reload.Is(Input.GetKeyDown), State.Reload);
        }

        internal override Vector3 FacingDirection() {
            return Quaternion.AngleAxis(pitch, Vector3.right) * cam.transform.forward;
        }
    }
}