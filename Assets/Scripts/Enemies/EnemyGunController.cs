using Gun;

namespace Enemies {
    public class EnemyGunController : GunController {
        public EnemyPlayerDetection detection;

        public void Update() {
            CurrentGun.WantsToShoot(detection.canSeePlayer);
            CurrentGun.DoReload = CurrentGun.Ammo == 0;
        }
    }
}