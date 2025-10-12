using UnityEngine;

namespace Gun.Behaviours {
    public class Unequip : StateMachineBehaviour {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.gameObject.GetComponent<Gun>().whenUnequipped!.Invoke();
        }
    }
}