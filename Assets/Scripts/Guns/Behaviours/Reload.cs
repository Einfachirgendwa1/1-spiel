using UnityEngine;

namespace Guns.Behaviours {
    public class Reload : Base {
        internal override State State => State.Reload;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            Gun.audioSource.PlayOneShot(Gun.reloadSound);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            Gun.Reload();
        }
    }
}