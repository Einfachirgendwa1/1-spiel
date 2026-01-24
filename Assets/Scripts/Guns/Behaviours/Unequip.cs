using UnityEngine;

namespace Guns.Behaviours {
    public class Unequip : Base {
        internal override State State => State.Unequip;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            Gun.Controller.OnUnequip();
        }
    }
}