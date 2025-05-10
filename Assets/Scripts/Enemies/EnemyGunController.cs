using Gun;

namespace Enemies {
    public class EnemyGunController : Guns {
        public EnemyPlayerDetection detection;

        public void Update() {
            CurrentGun.ShouldShoot = detection.canSeePlayer;
            CurrentGun.ShouldReload = CurrentGun.Ammo == 0;
        }
    }
}