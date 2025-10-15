using UnityEngine;

namespace Gun.Behaviours {
    public class Shoot : StateMachineBehaviour {
        private Gun gun;
        private int shots;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            gun = animator.gameObject.GetComponent<Gun>();
            gun.Shoot();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            int shotsThisFrame = Mathf.FloorToInt(stateInfo.normalizedTime);
            if (shotsThisFrame > shots) {
                shots = shotsThisFrame;
                gun.Shoot();
            }
        }
    }
}