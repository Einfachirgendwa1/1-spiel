using UnityEngine;

namespace Guns.Behaviours {
    public class Unequip : Base {
        internal override State state => State.Unequip;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            gun.controller.OnUnequip();
        }
    }
}