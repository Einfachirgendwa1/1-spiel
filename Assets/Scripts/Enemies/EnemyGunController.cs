using Guns;
using UnityEngine;

namespace Enemies {
    public class EnemyGunController : GunController {
        private static readonly int IsReloading = Animator.StringToHash("isReloading");
        public Animator enemyAnim;

        public new void Start() {
            base.Start();
            enemyAnim = GetComponent<Animator>();
        }

        public new void Update() {
            base.Update();
            enemyAnim.SetBool(IsReloading, CurrentGun.Ammo == 0);
            InputBuffer[State.Reload] = CurrentGun.Ammo == 0 ? 1 : 0;
        }
    }
}