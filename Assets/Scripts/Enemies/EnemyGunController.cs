using Guns;

namespace Enemies {
    public class EnemyGunController : GunController {
        public new void Update() {
            base.Update();
            InputBuffer[State.Reload] = CurrentGun.Ammo == 0 ? 1 : 0;
        }
    }
}