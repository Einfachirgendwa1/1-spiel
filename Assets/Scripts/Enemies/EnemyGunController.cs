using Guns;

namespace Enemies {
    public class EnemyGunController : GunController {
        public EnemyPlayerDetection detection;

        public new void Update() {
            base.Update();
            InputBuffer[State.Shoot] = detection.State == EnemyState.Attacking ? 1 : 0;
            InputBuffer[State.Reload] = CurrentGun.Ammo == 0 ? 1 : 0;
        }
    }
}