using Guns;
using UnityEngine;

namespace Enemies {
    public class EnemyGunController : GunController {

        public Animator enemyAnim;

        public void Start()
        {
            base.Start();
            enemyAnim = GetComponent<Animator>();
        }
        public new void Update() {
            base.Update();
            enemyAnim.SetBool("isReloading", CurrentGun.Ammo == 0 );
            InputBuffer[State.Reload] = CurrentGun.Ammo == 0 ? 1 : 0;
        }
    }
}