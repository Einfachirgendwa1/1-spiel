using UnityEngine;

namespace Gun.Behaviours {
    public class Reload : StateMachineBehaviour {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            Gun gun = animator.gameObject.GetComponent<Gun>();
            gun.Ammo = gun.magazineSize;
        }
    }
}