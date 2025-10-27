using Guns;

namespace Enemies {
    public class EnemyGunController : GunController {
        public EnemyPlayerDetection detection;

        public new void Update() {
            base.Update();
            inputBuffer[State.Shoot] = detection.canSeePlayer ? 1 : 0;
            inputBuffer[State.Reload] = CurrentGun.Ammo == 0 ? 1 : 0;
        }
    }
}